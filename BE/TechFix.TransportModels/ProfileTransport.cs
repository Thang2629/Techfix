using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using TechFix.Common.Enums;
using TechFix.Common.Interfaces;
using TechFix.EntityModels;

namespace TechFix.TransportModels
{
	public class ProfileTransport : IMapFrom<User>
	{
		public string FullName { get; set; }
		public string LastName { get; set; }
		public string Username { get; set; }
		public DateTime? BirthDay { get; set; }
		public string Country { get; set; }
		public string ZipCode { get; set; }
		public string PhoneNumber { get; set; }
		public string DialCode { get; set; }
		public string Email { get; set; }
		public string KYC { get; set; }
		public string VLinkId { get; set; }
		public bool? ExistPin { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Address { get; set; }
		public string Status { get; set; }
		public string SocialSecurityCode { get; set; }
		public string Type { get; set; }
		public string BusinessDescription { get; set; }
		public string BusinessLogo { get; set; }
		public string BusinessLatitude { get; set; }
		public string BusinessLongitude { get; set; }
		public string BusinessName { get; set; }
		public string BusinessWebsite { get; set; }
		public string Avatar { get; set; }
		public string ReferralId { get; set; }
		public string ReferralUserName { get; set; }
		public string ReferralEmail { get; set; }
		public string ReferralPhoneNumber { get; set; }
		public string Title { get; set; }
        public string ReferralDialCode { get; set; }
    }
}
