using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechFix.Common.Interfaces;
using TechFix.EntityModels;

namespace TechFix.TransportModels.Dtos
{
    public class FixOrderDto : IMapFrom<FixOrder>
    {
        public string Code { get; set; }

        public string CustomerPhoneNumber { get; set; }

        public string CustomerName { get; set; }

        public string CustomerGroup { get; set; }

        public DateTime ReceivedDate { get; set; }

        public string StoreName { get; set; }

        public string CashierName { get; set; }

        public int TotalItems { get; set; }

        public List<FixProductDto> FixProducts { get; set; }
    }
}
