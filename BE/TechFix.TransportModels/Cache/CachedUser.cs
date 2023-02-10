using System;
using TechFix.EntityModels;

namespace TechFix.TransportModels.Cache
{
    public class CachedUser
    {
        public User User { get; set; }
        public UserToken ValidateToken { get; set; }
        public DateTime? LastRefresh { get; set; }
    }
}
