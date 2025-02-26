namespace EmailVerificationAPI.Services
{
    public class VerificationService
    {
        private static Dictionary<string, (string Code, DateTime ExpirationTime)> verificationCodes = new();

        public static void AddVerificationCode(string email, string code)
        {
            // GEçerlilik süresi 5 dakika (300 saniye) olarak ayarlandı
            DateTime expirationTime = DateTime.Now.AddMinutes(5);
            //DateTime expirationTime = DateTime.Now.AddSeconds(20); // Alternatif olarak süre cinsinden 
            verificationCodes[email] = (code, expirationTime);
        }

        public static bool IsCodeValid(string email, string code)
        {
            // e-posta adresi bulunamazsa false döner
            if(!verificationCodes.ContainsKey(email))
                return false;

            var (storedCode, expirationTime) = verificationCodes[email];

            // kod süresi geçtiyse false dmner
            if(DateTime.Now > expirationTime)
            {
                verificationCodes.Remove(email); // kodun süresi geçtiği için sil
                return false;
            }

            // kod eşleşiyorsa true döner
            return storedCode == code;
        }

        public static void RemoveVerificationCode(string email)
        {
            if (verificationCodes.ContainsKey(email)) 
            {
                verificationCodes.Remove(email);
            }
        }
    }
}
