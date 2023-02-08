using Managerment.CustomerTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Managerment.OrderWarranties
{
    public class ListOrderWarrantyDto
    {
        public string OW_code { get; set; }
        public string Phone { get; set; }
        public string Customer_name { get; set; }
        public ECustomerTypeEnum Customer_type { get; set; }
        public string Customer_type_name { get; set; }
        public DateTime Received_time { get; set; }
        public string Store_name { get; set; }
        public string User_name { get; set; }
        public int Total_count { get; set; }
    }
}
