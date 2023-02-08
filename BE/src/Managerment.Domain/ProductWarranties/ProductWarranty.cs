using Managerment.OrderWarranties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Managerment.ProductWarranties
{
    public class ProductWarranty : AuditedAggregateRoot<Guid>
    {
        public string PW_code { get; set; }
        public string PW_name { get; set; }
        public DateTime PW_date_finish { get; set; }
        public string PW_status { get; set; }
        public EProductWarrantyType Product_warranty_type { get; set; }
        public int Warranty_times { get; set; }
        public int Total_count { get; set; }
        public Guid ID_detail { get; set; }
        public Guid ID_order_warranty { get; set; }
        private ProductWarranty()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal ProductWarranty(
               Guid id,
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
           : base(id)
        {
            PW_code = pw_code;
            PW_name = pw_name;
            PW_date_finish = pw_date_finish;
            PW_status = pw_status;
            Product_warranty_type = product_warranty_type;
            Warranty_times = warranty_times;
            Total_count = total_count;
            ID_detail = id_detail;
            ID_order_warranty = id_order_warranty;
        }
    }
}
