using System.ComponentModel.DataAnnotations;

namespace TechFix.TransportModels.Auth
{
	public class LoginTransport
	{
		[Required] public string Username { get; set; }

		[Required] public string Password { get; set; }

		public string ReCaptchaToken { get; set; }
		public string GoogleCode { get; set; }
		public string SmsCode { get; set; }
		public string EmailCode { get; set; }
	}
}
