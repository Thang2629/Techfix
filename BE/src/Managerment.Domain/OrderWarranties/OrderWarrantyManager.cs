using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Managerment.OrderWarranties
{
    public class OrderWarrantyManager : DomainService
    {
        public OrderWarranty CreateAsync(
              string ow_code,
              string text,
              Guid id_cus,
              Guid id_user
  )
        {
            return new OrderWarranty(
                GuidGenerator.Create(),
                ow_code,
                text,
                id_cus,
                id_user
            );
        }
    }
}
