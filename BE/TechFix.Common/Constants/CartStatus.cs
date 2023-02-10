using System.Collections.Generic;

namespace TechFix.EntityModels
{
    public class CartStatus
    {
        public const string Open = "OPEN";
        public const string Pending = "PENDING";
        public const string Rejected = "REJECTED";
        public const string Approved = "APPROVED";
        public const string Expried = "EXPRIED";
        public static List<string> HistoryStatus = new() { Pending, Rejected, Approved, Expried };
    }
}