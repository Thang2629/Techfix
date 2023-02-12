using System;

namespace TechFix.TransportModels
{
	public class UpdateMemberTransport
	{
		public Guid? Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime? BirthDay { get; set; }
		public string Country { get; set; }
		public string ZipCode { get; set; }
		public string PhoneNumber { get; set; }
        public string DialCode { get; set; }
		public string Email { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Address { get; set; }
		public string Status { get; set; }
		public string ReferralId { get; set; }
		public string Gender { get; set; }
		public string Type { get; set; }
		public string BusinessDescription { get; set; }
		public string BusinessLogo { get; set; }
		public string BusinessLatitude { get; set; }
		public string BusinessLongitude { get; set; }
		public string BusinessName { get; set; }
		public string BusinessWebsite { get; set; }
	}
}
