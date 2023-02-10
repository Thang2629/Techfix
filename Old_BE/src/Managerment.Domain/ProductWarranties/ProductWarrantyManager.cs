using Managerment.OrderWarranties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Managerment.ProductWarranties
{
    public class ProductWarrantyManager : DomainService
    {
        public ProductWarranty CreateAsync(
               string pw_code,
               string pw_name,
               DateTime pw_date_finish,
               string pw_status,             
               EProductWarrantyType product_warranty_type,
               int warranty_times,
               int total_count,
               Guid id_detail,
               Guid id_order_warranty
    )
        {
            return new ProductWarranty(
                GuidGenerator.Create(),
                pw_code,
                pw_name,
                pw_date_finish,
                pw_status,
                product_warranty_type,
                warranty_times,
                total_count,
                id_detail,
                id_order_warranty
            );
        }
    }
}
