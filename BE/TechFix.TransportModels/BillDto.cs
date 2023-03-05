using System;
using TechFix.Common.Interfaces;
using TechFix.EntityModels;

namespace TechFix.TransportModels
{
    public class BillDto : IMapTo<Bill>
    {
        public Guid? Id { get; set; }
        public string PhoneNumber { get; set; }
        public string CustomerName { get; set; }
        public string Code { get; set; }
        public string StoreName { get; set; }
        public Guid? SellerId { get; set; }
        public string Note { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalQuantity { get; set; }
        public decimal AmountOwed { get; set; }
        public DateTime SaleDate { get; set; }
        public string SaleName { get; set; }
    }
}
