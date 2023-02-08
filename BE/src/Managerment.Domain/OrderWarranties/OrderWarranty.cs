using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Managerment.OrderWarranties
{
    public class OrderWarranty : AuditedAggregateRoot<Guid> 
    {
        public string OW_code { get; set; }
        public string Text { get; set; }
        public Guid ID_cus { get; set; }
        public Guid ID_user { get; set; }
        private OrderWarranty()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal OrderWarranty(
              Guid id,
              string ow_code,
              string text,
              Guid id_cus,
              Guid id_user
           )
           : base(id)
        {
            OW_code = ow_code;
            Text = text;
            ID_cus = id_cus;
            ID_user = id_user;
        }
    }
}
