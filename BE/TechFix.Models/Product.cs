using System;

namespace TechFix.EntityModels
{
    public class Product : BaseModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int MinimumNorm { get; set; }
        public int MaximumNorm { get; set; }
        public decimal OriginalCost { get; set; }
        public decimal SellIn { get; set; }
        public decimal SellOut { get; set; }
        public string Warranty { get; set; }
        public string Description { get; set; }
        public bool IsInventoryTracking { get; set; }
        public bool AllowNegativeSell { get; set; }
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

        public Store Store { get; set; }
        public Guid? StoreId { get; set; }
    }
}
