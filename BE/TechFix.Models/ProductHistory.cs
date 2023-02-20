using System;
namespace TechFix.EntityModels
{
    public class ProductHistory : BaseModel
    {
        public Guid UserId { get; set; }
        public string ActionName { get; set; }
        public string Code { get; set; }
        public int Quantity { get; set; }
        public string ProductStatus { get; set; }
        public string Warranty { get; set; }
        public Guid StoreId { get; set; }
        public Store Store { get; set; }
    }
}
