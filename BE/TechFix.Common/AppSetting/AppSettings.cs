namespace TechFix.Common.AppSetting
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string ImagePath { get; set; }
        public string EncryptionKey { get; set; }
        public string CurrentUrl { get; set; }
        public string VerifyApiAddress { get; set; }
        public string SiteKey { get; set; }
        public string SecretKey { get; set; }
        public string RecaptchaThreshold { get; set; }
        public bool IsCheckReCaptcha { get; set; }
        public decimal VLGPlusToUsd { get; set; }
        public string SenderEmail { get; set; }
        public string PassSenderEmail { get; set; }
        public bool IsDevelopment { get; set; }
        public string DefaultReferralUser { get; set; }
    }
}
