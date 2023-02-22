using System;
using TechFix.EntityModels.Handle;

namespace TechFix.EntityModels
{
    [EntityClass(FullTextSearch = true)]
    public class Product : BaseModel
    {
        #region FK

        public Manufacturer Manufacturer { get; set; }
        public Guid? ManufacturerId { get; set; }

        public Supplier Supplier { get; set; }
        public Guid? SupplierId { get; set; }

        public Category Category { get; set; }
        public Guid? CategoryId { get; set; }

        public ProductCondition ProductCondition { get; set; }
        public Guid? ProductConditionId { get; set; }

        public ProductUnit ProductUnit { get; set; }
        public Guid? ProductUnitId { get; set; }

        #endregion
        [DataColumn(AllowSearch = true)]
        public string Code { get; set; }

        [DataColumn(AllowSearch = true)]
        public string Name { get; set; }

        public int Quantity { get; set; }
        public int MinimumNorm { get; set; }
        public int MaximumNorm { get; set; }
        /// <summary>
        /// Giá Nhập : Giá nhập hàng vào
        /// </summary>
        public decimal OriginalPrice { get; set; }
        /// <summary>
        /// Giá Web  : Giá bán ra
        /// </summary>
        public decimal WebPrice { get; set; }
        /// <summary>
        /// Giá Vốn  : Giá hàng Fake chỉ show chụp hình cho user xem chơi
        /// </summary>
        public decimal FakePrice { get; set; }
        public string Warranty { get; set; }
        public string Description { get; set; }
        public bool IsInventoryTracking { get; set; }
        public bool AllowNegativeSell { get; set; }
        public bool Discontinue { get; set; }
        public string ImagePath { get; set; }
    }
}
