using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechFix.TransportModels.Auth
{
	public class TokenTransport
	{
		//[JsonProperty("access_token")]
		//public string AccessToken { get; set; }
		//[JsonProperty("refresh_token")]
		//public string RefreshToken { get; set; }
		//[JsonProperty("expires_in")]
		//public DateTime? ExpiresIn { get; set; }

		public string Type { get; set; }
		public Guid? Id { get; set; }
		public string Username { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Role { get; set; }
		public string Token { get; set; }
		public string Email { get; set; }
		public string ImagePath { get; set; }
		public string Status { get; set; }
		public List<string> AuthenticationTypes { get; set; }
		public string Avatar { get; set; }
    }
}
