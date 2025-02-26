# E-posta Doğrulama API'si 📧

[English](README.md) | [Türkçe](README_TR.md)

Bu API, kullanıcıların e-posta adreslerini doğrulamak için kullanılan basit ve etkili bir .NET Core Web API projesidir.

## Özellikler ✨

- E-posta adresine 6 haneli doğrulama kodu gönderme
- Doğrulama kodunun geçerliliğini kontrol etme
- 5 dakikalık kod geçerlilik süresi
- Gmail SMTP entegrasyonu
- Swagger UI desteği

## Teknolojiler 🛠

- .NET 8.0
- MailKit (E-posta gönderimi için)
- Swagger/OpenAPI
- Memory Cache

## Kurulum 🚀

1. Repoyu klonlayın:
   ```bash
   git clone https://github.com/kullaniciadi/EmailVerificationAPI.git
   ```

2. Proje dizinine gidin:
   ```bash
   cd EmailVerificationAPI
   ```

3. `VerificationController.cs` dosyasında Gmail bilgilerinizi güncelleyin:
   ```csharp
   emailMessage.From.Add(new MailboxAddress("Destek", "sizin-mailiniz@gmail.com"));
   await smtp.AuthenticateAsync("sizin-mailiniz@gmail.com", "uygulama-şifreniz");
   ```

4. Projeyi çalıştırın:
   ```bash
   dotnet run
   ```

## API Endpoint'leri 📝

### 1. Doğrulama Kodu Gönderme
http
POST /api/Verification/send-code
Content-Type: application/json
{
"email": "kullanici@ornek.com"
}


### 2. Doğrulama Kodunu Kontrol Etme
http
POST /api/Verification/verify-code
Content-Type: application/json
{
"email": "kullanici@ornek.com",
"code": "123456"
}



## Güvenlik 🔒

- Doğrulama kodları 5 dakika sonra otomatik olarak geçersiz olur
- Başarılı doğrulama sonrası kodlar sistemden silinir
- Memory Cache kullanılarak geçici veri depolama

## Katkıda Bulunma 🤝

1. Bu repoyu fork edin
2. Yeni bir branch oluşturun (`git checkout -b feature/yeniOzellik`)
3. Değişikliklerinizi commit edin (`git commit -am 'Yeni özellik eklendi'`)
4. Branch'inizi push edin (`git push origin feature/yeniOzellik`)
5. Pull Request oluşturun

## Lisans 📄

Bu proje MIT lisansı altında lisanslanmıştır. Daha fazla bilgi için [LICENSE](LICENSE) dosyasına bakın.

## İletişim 📧

Sorularınız için yasardgn99@gmail.com
