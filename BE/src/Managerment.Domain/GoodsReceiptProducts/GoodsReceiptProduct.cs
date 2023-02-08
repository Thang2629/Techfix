using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Managerment.GoodsReceiptProducts
{
    public class GoodsReceiptProduct : AuditedAggregateRoot<Guid>
    {
        public Guid Goods_receipt_id { get; set; }
        public Guid Product_id { get; set; }
        public decimal Import_price { get; set; }
        public int Total { get; set; }
        private GoodsReceiptProduct()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal GoodsReceiptProduct(
         Guid id,
         Guid goods_receipt_id,
         Guid product_id,
         decimal import_price,
         int total
         )
         : base(id)
        {
            Goods_receipt_id = goods_receipt_id;
            Product_id = product_id;
            Import_price = import_price;
            Total = total;
        }
    }
}
