
using System.ComponentModel.DataAnnotations;

namespace TechFix.TransportModels.Auth
{
	public class DisableTwoFactorAuthTransport
	{
		[Required] public string Type { get; set; }
		[Required] public string Code { get; set; }
	}
}
