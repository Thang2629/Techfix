using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechFix.Common.Interfaces;
using TechFix.EntityModels;

namespace TechFix.TransportModels.Dtos
{
    public class IncomeTicketDto : IMapFrom<IncomeTicket>
    {
        [JsonIgnore]
        public Guid? Id { get; set; }

        public string Code { get; set; }

        public Guid? ExportId { get; set; }

        public string ExportCode { get; set; }

        public string PhoneNumber { get; set; }

        public Guid? SupplierId { get; set; }

        public string SupplierCode { get; set; }

        public string ImageUrl { get; set; }

        public Guid? StoreId { get; set; }

        public string StoreName { get; set; }

        public DateTime? PaymentDate { get; set; }

        public Guid? Cashier { get; set; }

        public string CashierName { get; set; }

        public Guid? PaymentTypeId { get; set; }

        public string PaymentTypeName { get; set; }

        public string Note { get; set; }

        public decimal Debt { get; set; }

        public decimal Amount { get; set; }

    }
}
