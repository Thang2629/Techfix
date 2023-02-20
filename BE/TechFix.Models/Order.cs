using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechFix.EntityModels
{
    public class Order : BaseModel
    {
        #region FK

        public Customer Customer { get; set; }
        public Guid? CustomerId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Guid? PaymentMethodId { get; set; }

        public User Seller { get; set; }
        public Guid? SellerId { get; set; }

        #endregion

        public string Note { get; set; }
        public decimal Vat { get; set; }
        public decimal TotalGoodsMoney { get; set; }
        public int TotalQuantity { get; set; }
        public decimal DiscountPerItem { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal AmountOwed { get; set; }
    }
}
