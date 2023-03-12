
using System;

namespace TechFix.EntityModels
{
    public class Fund : BaseModel
    {
        #region MyRegion

        public Store Store { get; set; }
        public Guid? StoreId { get; set; }
        public Guid? CashierId { get; set; } // login user
        public User Cashier { get; set; }

        #endregion
        public string Code { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsAdd { get; set; } // true: Quỹ thu; false: Quỹ chi
        public string ImageUrl { get; set; }
    }
}
