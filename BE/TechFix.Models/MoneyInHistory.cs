using System;
using TechFix.EntityModels.Abstracts;

namespace TechFix.EntityModels
{
    public class MoneyInHistory : MoneyHistory
    {
        #region FK

        public Guid? CustomerId { get; set; }
        public Customer Customer { get; set; }

        public Guid? BillId { get; set; }
        public Bill Bill { get; set; }
        #endregion
    }
}
