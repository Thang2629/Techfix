using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TechFix.TransportModels.Dtos
{
    public class RepairProductByFixStaffViewDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal TotalMoney { get; set; }
        //public string SearchData { get; set; }
        public List<FixProductViewDto> FixProducts { get; set; }

    }
}
