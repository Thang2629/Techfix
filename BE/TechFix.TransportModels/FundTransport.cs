using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechFix.TransportModels
{
    public class FundTransport
    {
        public Guid? Cashier { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAdd { get; set; }
        public string Note { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public Guid? StoreId { get; set; }
    }
}
