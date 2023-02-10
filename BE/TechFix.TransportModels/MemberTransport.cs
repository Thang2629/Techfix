using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechFix.TransportModels
{
 	public class MemberTransport
	{
		public Guid? Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Username { get; set; }
		public string Status { get; set; }
		public string PhoneNumber { get; set; }
        public string DialCode { get; set; }
		public string Gender { get; set; }
		public string Address { get; set; }
		public DateTime? Birthday { get; set; }
		//public bool? KYC { get; set; }
		public string Country { get; set; }
		public string ZipCode { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string ReferralId { get; set; }
		public string VLinkId { get; set; }
		public string Type { get; set; }
		public string BusinessDescription { get; set; }
		public string BusinessLogo { get; set; }
		public string BusinessLatitude { get; set; }
		public string BusinessLongitude { get; set; }
        public string BusinessName { get; set; }
        public string BusinessWebsite { get; set; }
	}
}
