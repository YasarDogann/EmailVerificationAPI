using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Net.Mail;
using MailKit.Net.Smtp;
using EmailVerificationAPI.Services;


namespace EmailVerificationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationController : ControllerBase
    {
        /*
         Bu API, kullanıcının e-posta adresine doğrulama kodu göndermek ve ardından girilen kodun doğruluğunu kontrol etmek için iki farklı endpoint içeriyor:

        send-code → Kullanıcının e-posta adresine rastgele 6 haneli bir doğrulama kodu gönderir.
        verify-code → Kullanıcının gönderdiği kodun geçerli olup olmadığını kontrol eder.
        */


        // Kullanıcıların e-posta adresi ve doğrulama kodlarını saklamak için Dictionary kullandım.
        private static Dictionary<string, string> verificationCodes = new();

        [HttpPost("send-code")]
        public async Task<IActionResult> SendVerificationCode([FromBody] EmailRequest request)
        {
            // e-posta adresi boşsa hata döndür
            if (string.IsNullOrEmpty(request.Email))
                return BadRequest(new { message = "Geçerli bir e-posta adresi girin" });

            // 6 haneli rastgele doğrulama kodu oluştur
            Random random = new Random();
            string verificationCode = random.Next(100000, 999999).ToString();

            // verification service üzerinden kodu sakla
            VerificationService.AddVerificationCode(request.Email, verificationCode);

            try
            {
                // yeni bir e-posta mesajı oluştur
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("Destek", "Mail-Adresiniz@gmail.com")); // gönderen e-posta adresi
                emailMessage.To.Add(new MailboxAddress("", request.Email)); // Alıcı e-posta adresi
                emailMessage.Subject = "E-posta Doğrulama Kodu"; // e-posta başlığı
                emailMessage.Body = new TextPart("plain")
                {
                    Text = $"Merhaba, doğrulama kodunuz: {verificationCode}" // epposta içeriği
                };

                // Gmail SMTP sunucusu üzerinden e-posta gönderme işlemi
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                await smtp.ConnectAsync("smtp.gmail.com", 587, false); // SMTP sunucusuna bağlan
                await smtp.AuthenticateAsync("Mail-Adresiniz@gmail.com", "Mail Özel Şifre"); // Gmail kimlik doğrulama
                await smtp.SendAsync(emailMessage); // e-postayı gönder
                await smtp.DisconnectAsync(true); // bağlantıyı kapat

                // başarılı mesajını döndür
                return Ok(new
                {
                    message = "Kod başarıyla gönderildi"
                });
            }
            catch (Exception ex)
            {
                //eğer hata oluşursa hata mesajını döndür
                return StatusCode(500, new { message = "E-posta gönderilirken hata oluştu", error = ex.Message });
            }
        }


        // Kullanıcının gönderdiği doğrulama kodunu kontrol eden endpoint
        [HttpPost("verify-code")]
        public IActionResult VerifyCode([FromBody] VerificationRequest request)
        {
            // kullanıcının girdiği e-posta ve kod veritabanımızda varsa ve eşleşiyorsa doğrula
            if (VerificationService.IsCodeValid(request.Email, request.Code))
            {
                // Kod doğruysa, verification kodunu sil
                VerificationService.RemoveVerificationCode(request.Email);
                return Ok(new { message = "E-posta başarıyla doğrulandı" });
            }

            // eğer kod yanlışsa hata mesajı döndür
            var message = "Geçersiz veya süresi dolmuş kod!";
            if(!VerificationService.IsCodeValid(request.Email, request.Code))
            {
                message = "Bu kodun süresi dolmuş. Lütfen yeni bir doğrulama kodu talep edin.";
            }
            return BadRequest(new { message = message});
        }

        // Kullanıcının e-posta adresini alan request ==> TODO : Ayrı dosyada request tutulup dto ile veri tabanından kontrol edilebilir
        public class EmailRequest
        {
            public string Email { get; set; }
        }

        // kullanıcının doğrulama kodu ve e-posta adresini göndermek için kullanılan request 
        public class VerificationRequest
        {
            public string Code { get; set; }
            public string Email { get; set; }
        }
    }
}
