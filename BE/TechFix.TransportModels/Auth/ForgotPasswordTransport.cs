using System.ComponentModel.DataAnnotations;

namespace TechFix.TransportModels.Auth
{
	public class ForgotPasswordTransport
	{
		[Required]
		public string Username { get; set; }

		public string ReCaptchaToken { get; set; }
    }
}
