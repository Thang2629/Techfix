using System;
using TechFix.EntityModels.Handle;

namespace TechFix.EntityModels
{
    public enum UserStatus
    {
        Active = 0,
        Inactive = 1
    }

    public class User : BaseModel
    {
        public string FullName { get; set; }
        public string StaffCode { get; set; }
        public string Email { get; set; }
        public decimal BonusPercent { get; set; }
        public string Role { get; set; }
        public Guid StoreId { get; set; }
        public UserStatus Status { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }
    }
}