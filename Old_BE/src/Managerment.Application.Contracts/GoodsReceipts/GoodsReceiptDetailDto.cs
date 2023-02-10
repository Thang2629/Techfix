using Managerment.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Managerment.GoodsReceipts
{
    public class GoodsReceiptDetailDto
    {
        public int Product_count { get; set; }
        public decimal Commodity_price { get; set; }
        public decimal Discount { get; set; }
        public decimal Total_price { get; set; }
        public decimal Total_debt { get; set; }
        public List<ProductDto> Product_lst { get; set; }
    }
}
