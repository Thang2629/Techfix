using System;
using System.Collections.Generic;

namespace TechFix.EntityModels
{
    /// <summary>
    /// Mua Product hoặc sửa chữa 
    /// </summary>
    public class OrderItem : BaseModel
    {
        #region FK

        public Order Order { get; set; }
        public Guid? OrderId { get; set; }

        public Product Product { get; set; }
        public Guid? ProductId { get; set; }

        public List<FixProduct> RepairProducts { get; set; }

        #endregion

        public string ProductSerial { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Amount => Quantity * Price;
        public DateTime? WarrantyPeriod { get; set; }
    }
}
