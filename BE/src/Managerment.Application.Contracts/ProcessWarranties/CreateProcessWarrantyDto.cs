using Managerment.WarrantyProcesss;
using System;
using System.Collections.Generic;
using System.Text;

namespace Managerment.ProcessWarranties
{
    public class CreateProcessWarrantyDto
    {
        public string Warranty_status { get; set; }
        public DateTime Warranty_date { get; set; }
        public EWarrantyProcess Warranty_process { get; set; }
        public string User_warranty { get; set; }
        public Guid Order_warranty_id { get; set; }
    }
}
