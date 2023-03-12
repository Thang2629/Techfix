using System;
using System.Collections.Generic;

namespace TechFix.EntityModels
{
    public class InputProduct : BaseModel
    {
        #region MyRegion

        public Supplier Supplier { get; set; }
        public Guid? SupplierId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
        public Guid? PaymentMethodId { get; set; }

        public List<InputProductItem> InputProductItems { get; set; }

        public User User { get; set; }
        public Guid? UserId { get; set; }

        #endregion

        public DateTime InputDate { get; set; }
        public string Note { get; set; }

        public decimal TotalGoodsMoney { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal AmountOwed { get; set; }
    }
}
