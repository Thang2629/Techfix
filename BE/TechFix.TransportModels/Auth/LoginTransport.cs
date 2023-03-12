using System.ComponentModel.DataAnnotations;

namespace TechFix.TransportModels.Auth
{
	public class LoginTransport
	{
		[Required] public string Username { get; set; }

		[Required] public string Password { get; set; }
	}
}
