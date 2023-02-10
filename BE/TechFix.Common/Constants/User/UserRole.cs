using System.Collections.Generic;

namespace TechFix.Common.Constants.User
{
    public class UserRole
    {
        public const string Admin = "ADMIN";
        public const string Member = "MEMBER";
        public const string Supporter = "SUPPORTER";
        public static List<string> AllRoles => new List<string> { Admin, Member, Supporter };
    }
}
