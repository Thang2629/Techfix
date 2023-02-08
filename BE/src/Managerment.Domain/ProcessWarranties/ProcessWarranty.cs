using Managerment.WarrantyProcesss;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Managerment.ProcessWarranties
{
    public class ProcessWarranty : AuditedAggregateRoot<Guid>
    {
        public string Warranty_status { get; set; }
        public DateTime Warranty_date { get; set; }
        public EWarrantyProcess Warranty_process { get; set; }
        public decimal Warranty_price { get; set; }
        public string User_warranty { get; set; }
        public Guid Order_warranty_id { get; set; }
        private ProcessWarranty()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal ProcessWarranty(
               Guid id,
               string warranty_status,
               DateTime warranty_date,
               decimal warranty_price,
               string user_warranty,
               [NotNull]EWarrantyProcess warranty_process,
               [NotNull]Guid order_warranty_id
           )
           : base(id)
        {
            Warranty_status = warranty_status;
            Warranty_date = warranty_date;
            Warranty_price = warranty_price;
            User_warranty = user_warranty;
            Warranty_process = warranty_process;
            Order_warranty_id = order_warranty_id;
        }
    }
}
