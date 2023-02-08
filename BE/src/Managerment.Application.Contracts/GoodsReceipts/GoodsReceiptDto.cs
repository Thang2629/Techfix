using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Managerment.GoodsReceipts
{
    public class GoodsReceiptDto : EntityDto<Guid>
    {
        public string Supplier_name { get; set; }
        public string Receipt_code { get; set; }
        public string Store_name { get; set; }
        public string User_name { get; set; }
        public DateTime Receipt_date { get; set; }
        public decimal Total_price { get; set; }
        public decimal Debt { get; set; }  
    }
}
