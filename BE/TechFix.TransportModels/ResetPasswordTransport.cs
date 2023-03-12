
namespace TechFix.TransportModels
{
    public class ResetPasswordTransport
    {
        public string Password { get; set; }
        public string PasswordAgain { get; set; }
        public string ReCaptchaToken { get; set; }
    }
}
