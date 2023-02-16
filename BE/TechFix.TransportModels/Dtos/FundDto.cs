using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechFix.Common.Interfaces;
using TechFix.EntityModels;

namespace TechFix.TransportModels.Dtos
{
    public class FundDto : IMapFrom<Fund>
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
        public Guid? Cashier { get; set; } // login user
        public DateTime PaymentDate { get; set; }
        public bool IsAdd { get; set; } // true: Quỹ thu; false: Quỹ chi
        public string ImageUrl { get; set; }
        public string StoreName { get; set; }
    }
}
