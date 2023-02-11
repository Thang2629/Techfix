using System;
using System.Collections.Generic;

namespace TechFix.TransportModels.Auth
{
	public class TokenTransport
	{
		public Guid? Id { get; set; }
		public string Username { get; set; }
		public string FullName { get; set; }
		public string Role { get; set; }
		public string Token { get; set; }
		public string Email { get; set; }
		public string Status { get; set; }
		public List<string> AuthenticationTypes { get; set; }
    }
}
