
using System.Collections.Generic;

namespace TechFix.Common.Constants
{
    public class MonthlyInterestStatus
    {
        public const string Open = "Open" ;
        public const string Paid = "Paid";
        public const string IndDebt = "In Debt";
        public const string Closed = "Closed";
        public static List<string> ActiveStatusList = new() { Open, IndDebt };
    }
}
