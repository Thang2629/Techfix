using System.Collections.Generic;

namespace TechFix.Common.Constants.User
{
    public class UserRole
    {
        public const string Admin = "ADMIN";
        public const string Manager = "MANAGER";
        public const string Staff = "STAFF";
        public const string AllRole = "ADMIN, MANAGER, STAFF";
        public static List<string> AllRoles => new() {Admin, Manager, Staff};
    }
}
