
using System;
using System.ComponentModel.DataAnnotations;

namespace TechFix.EntityModels
{
    public class IncomeTicket : BaseModel
    {
        public string Code { get; set; }
        //public Guid? ExportId { get; set; }
        //public Export Export { get; set; }
        [MaxLength(11), MinLength(10)]
        public string PhoneNumber { get; set; }
        public Guid? CashierId { get; set; }
        public User Cashier { get; set; }
        public string ImageUrl { get; set; }
        public string Note { get; set; }
        public decimal Amount { get; set; }
        public decimal Debt { get; set; }
        public DateTime? PaymentDate { get; set; }
        public Guid? PaymentTypeId { get; set; }
        //public PaymentType PaymentType { get; set; }
        public Guid? SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public Guid? StoreId { get; set; }
        public Store Store { get; set; }
    }
}
