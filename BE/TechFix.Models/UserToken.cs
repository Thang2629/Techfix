using System;

namespace TechFix.EntityModels
{
    public class UserToken : BaseModel
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid Token { get; set; }
        public string Type { get; set; }
        public DateTime ExpiredDate { get; set; }
        public string Status { get; set; }
        public string IpAddress { get; set; }
        public class UserTokenStatus
        {
            public const string Active = "ACTIVE";
            public const string Inactive = "INACTIVE";
        }
    }
}
