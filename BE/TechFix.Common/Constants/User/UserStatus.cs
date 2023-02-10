using System.Collections.Generic;

namespace TechFix.Common.Constants.User
{
    public class UserStatus
    {
        public const string Active = "ACTIVE";
        public const string WaitingConfirm = "WAITING CONFIRM";
        public const string Inactive = "INACTIVE";
        public const string Deleted = "DELETED";
        public const string Lock = "LOCK";
        public static List<string> AllStatus => new List<string> { Active, WaitingConfirm, Inactive, Deleted, Lock };
    }
}
