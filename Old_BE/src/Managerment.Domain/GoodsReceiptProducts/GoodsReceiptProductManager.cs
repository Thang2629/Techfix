using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Managerment.GoodsReceiptProducts
{
    public class GoodsReceiptProductManager : DomainService
    {
        public GoodsReceiptProduct CreateAsync(
         Guid goods_receipt_id,
         Guid product_id,
         decimal import_price,
         int total
        )
    {
        return new GoodsReceiptProduct(
            GuidGenerator.Create(),
            goods_receipt_id,
            product_id,
            import_price,
            total
        );
    }
}
}
