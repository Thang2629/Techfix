using System;
using System.Collections.Generic;
namespace TechFix.TransportModels.Dtos
{
    public class PostBillDto
    {
        public Guid? StoreId { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? PaymentMethodId { get; set; }
        public string Note { get; set; }
        public Guid? SellerId { get; set; }
        public decimal Vat { get; set; }
        public decimal TotalGoodsMoney { get; set; }
        public int TotalQuantity { get; set; }
        public decimal DiscountPerItem { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal AmountOwed { get; set; }
        public List<PostBillItemDto> Products { get; set; }
        public List<PostBillItemDto> FixProducts { get; set; }
    }

    public class PostBillItemDto
    {
        public Guid? Id { get; set; }
        public string Serial { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime? WarrantyPeriod { get; set; }
    }
}
