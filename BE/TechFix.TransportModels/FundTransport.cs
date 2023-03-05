using System;

namespace TechFix.TransportModels
{
    public class FundTransport
    {
        public string ImageUrl { get; set; }
        public bool IsAdd { get; set; }
        public string Note { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public Guid? StoreId { get; set; }
    }
}
