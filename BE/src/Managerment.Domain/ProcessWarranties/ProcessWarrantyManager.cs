using Managerment.WarrantyProcesss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Managerment.ProcessWarranties
{
    public class ProcessWarrantyManager : DomainService
    {
        public ProcessWarranty CreateAsync(
               string warranty_status,
               DateTime warranty_date,
               decimal warranty_price,
               EWarrantyProcess warranty_process,
               string user_warranty,
               Guid order_warranty_id
    )
        {
            return new ProcessWarranty(
                GuidGenerator.Create(),
                warranty_status,
                warranty_date,
                warranty_price,
                user_warranty,
                warranty_process,
                order_warranty_id
            );
        }
    }
}
