using Managerment.ProcessRepairs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Managerment.OrderRepairs
{
    public class GroupProcessRepairDto
    {
        public int Checking_count { get; set; }
        public int Quote_count { get; set; }
        public int Fixing_count { get; set; }
        public int Fixed_count { get; set; }
        public int Return_customer { get; set; }
        public decimal Expected_revenue { get; set; }
        public Dictionary<Guid,List<DataProcessRepairDto>> Data_process_repair { get; set; }
    }
}
