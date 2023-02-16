using System;
using TechFix.EntityModels;

namespace TechFix.TransportModels
{
 	public class MemberTransport
	{
		public string FullName { get; set; }
		public string Email { get; set; }
        public decimal BonusPercent { get; set; }
        public string Role { get; set; }
		public UserStatus Status { get; set; }

	}
}
