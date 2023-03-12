using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechFix.EntityModels;

namespace TechFix.TransportModels
{
    public class IncomeTicketTransport
    {
        public string Code { get; set; }
        [MaxLength(11), MinLength(10)]
        public string PhoneNumber { get; set; }
        public string ImageUrl { get; set; }
        public string Note { get; set; }
        public decimal Amount { get; set; }
        public decimal Debt { get; set; }
        public DateTime? PaymentDate { get; set; }
        public Guid? PaymentTypeId { get; set; }
        public Guid? SupplierId { get; set; }
        public Guid? StoreId { get; set; }
        public Guid? CashierId { get; set; }
        public Guid? ExportId { get; set; } // chưa có bảng Export
    }
}
