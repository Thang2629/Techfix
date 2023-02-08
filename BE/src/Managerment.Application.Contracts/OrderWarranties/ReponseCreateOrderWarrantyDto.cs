using Managerment.ProductWarranties;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Managerment.OrderWarranties
{
    public class ReponseCreateOrderWarrantyDto : EntityDto<Guid>
    {
        public string OW_code { get; set; }
        public string Text { get; set; }
        public Guid ID_cus { get; set; }
        public Guid ID_user { get; set; }
    }
}
