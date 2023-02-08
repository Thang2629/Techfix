using System;
using System.Collections.Generic;
using System.Text;

namespace Managerment.GoodsReceipts
{
    public class GoodsReceiptProductDto
    {
        public Guid Product_id { get; set; }
        public int Total { get; set; }
        public decimal Import_price { get; set; }
    }
}
