using Managerment.OrderWarranties;
using Managerment.WarrantyProcesss;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Managerment.ProcessWarranties
{
    public class ProcessWarrantyDto 
    {
        public string Customer_name { get; set; }
        public string Order_warranty_code { get; set;}
        public string PW_code { get; set; }
        public string PW_name { get; set; }
        public int Warranty_times { get; set; }
        public decimal Warranty_price { get; set; }
        public EProductWarrantyType Product_warranty_type { get; set; }
        public string PW_status { get; set; }
        public string Warranty_status { get; set; }
        public DateTime Date_received { get; set; }
        public DateTime Warranty_date { get; set; }
        public EWarrantyProcess Warranty_process { get; set; }
        public string User_warranty { get; set; }

    }
}
