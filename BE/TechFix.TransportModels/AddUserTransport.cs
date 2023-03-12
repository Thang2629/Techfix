using System;
namespace TechFix.TransportModels
{
	public class AddUserTransport
	{
		public string FullName { get; set; }
        public string StaffCode { get; set; }
        public string Email { get; set; }
        public decimal BonusPercent { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public Guid StoreId { get; set; }
    }
}
