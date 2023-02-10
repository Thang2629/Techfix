using System.Collections.Generic;

namespace TechFix.Common.Constants.User
{
    public class UserType
    {
        public const string Affiliate = "AFFILIATE";
        public const string Customer = "CUSTOMER";
        public const string Business = "BUSINESS";
        public static List<string> AllType => new() { Affiliate, Customer, Business };
        public static List<string> LinkKycTypes => new() { Affiliate, Business };
    }
}
