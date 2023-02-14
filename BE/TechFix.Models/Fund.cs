
using System;

namespace TechFix.EntityModels
{
    public class Fund : BaseModel
    {
        public string Code { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
        public Guid? Cashier { get; set; } // login user
        public DateTime PaymentDate { get; set; }
        public bool IsAdd { get; set; } // true: Quỹ thu; false: Quỹ chi
        public Guid? StoreId { get; set; }
    }
}
