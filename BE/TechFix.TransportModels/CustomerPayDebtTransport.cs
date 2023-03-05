using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechFix.TransportModels
{
    public class CustomerPayDebtTransport
    {
        public Guid CustomerId { get; set; }
        public List<PayDebtTransport> PayDebtItem { get; set; }
    }
    public class PayDebtTransport
    {
        public Guid? BillId { get; set; }
        public decimal Amount { get; set; }

        public PayDebtTransport()
        {
            BillId = Guid.Empty;
            Amount = 0;
        }
    }
}
