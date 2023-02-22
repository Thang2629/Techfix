using System;
namespace TechFix.EntityModels
{
    public class ProductHistory : BaseModel
    {
        #region FK

        public Guid? StoreId { get; set; }
        public Store Store { get; set; }

        public Guid? ProductId { get; set; }
        public Product Product { get; set; }

        public Guid? ProductConditionId { get; set; }
        public ProductCondition ProductCondition { get; set; }

        #endregion
        public Guid? UserId { get; set; }
        public DateTime DateTime { get; set; }
        public string ActionName { get; set; }
        public string Code { get; set; }
        public int Quantity { get; set; }
        public string Warranty { get; set; }
        public decimal OriginalPrice { get; set; }
    }
}
