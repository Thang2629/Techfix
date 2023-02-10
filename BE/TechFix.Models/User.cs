using System;
using System.ComponentModel.DataAnnotations.Schema;
using TechFix.Common.Enums;
using TechFix.Common.Helper;
using TechFix.EntityModels.Handle;

namespace TechFix.EntityModels
{
    public class User : BaseModel
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        [DataColumn(IgnoreSearch = true)]
        public string PasswordHash { get; set; }
        [DataColumn(IgnoreSearch = true)]
        public string PasswordSalt { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public DateTime? Birthday { get; set; }
        public string Country { get; set; }
        [DataColumn(IgnoreSearch = true)]
		public string ReferralId { get; set; }
		public Guid? ReferralUserId { get; set; }
        public string VLinkId { get; set; }
        public string Avatar { get; set; }
        
        public string PhoneNumber { get; set; }
        public string DialCode { get; set; }
        public string CountryCode { get; set; } //country code for phone number
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }


    }
}
