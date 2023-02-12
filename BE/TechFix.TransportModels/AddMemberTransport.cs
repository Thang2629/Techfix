using System;

namespace TechFix.TransportModels
{
	public class AddMemberTransport
	{
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Username { get; set; }
		public string Gender { get; set; }
		public string Address { get; set; }
		public DateTime? Birthday { get; set; }
		public string Country { get; set; }
		public string ReferralId { get; set; }
        public string Type { get; set; }
	}
}
