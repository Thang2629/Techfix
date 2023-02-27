using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechFix.EntityModels;

namespace TechFix.TransportModels
{
    public class FixOrderTransport
    {
        public Guid? CustomerId { get; set; }

        public Guid? CashierId { get; set; }

        public Guid? StoreId { get; set; }

        public string Code { get; set; }

        public string Note { get; set; }

        public DateTime ReceivedDate { get; set; }

        public bool IsFixOrder { get; set; }

        public List<FixProductTransport> FixProducts { get; set; }

    }
}
