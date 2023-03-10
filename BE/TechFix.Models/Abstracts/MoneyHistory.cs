using System;

namespace TechFix.EntityModels.Abstracts
{
    public class MoneyHistory : BaseModel
    {

        #region FK
        public User Cashier { get; set; }
        public Guid? CashierId { get; set; }

        public Guid? PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        #endregion

        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
    }
}
