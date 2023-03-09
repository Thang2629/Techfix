using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechFix.TransportModels
{
    public class InputProductTransport
    {
        #region FK Reference
        public Guid? SupplierId { get; set; }
        public Guid? PaymentMethodId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? StoreId { get; set; }
        #endregion

        public DateTime? InputDate { get; set; }
        public string Note { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalGoodsMoney { get; set; }
        public decimal Discount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal AmountOwed { get; set; }
        public List<InputProductItemTransport> Items { get; set; }

    }

    public class InputProductItemTransport
    {
        public Guid? ProductId { get; set; }
        //public Guid? InputProductId { get; set; }
        public int Quantity { get; set; }
        public decimal OriginalPrice { get; set; }
    }
}
