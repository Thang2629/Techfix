using System;
using System.Collections.Generic;
using System.Text;

namespace Managerment.OrderWarranties
{
    public class CreateUpdateOrderWarrantyDto
    {
        public string OW_code { get; set; }
        public string Text { get; set; }
        public Guid ID_cus { get; set; }
        public Guid ID_user { get; set; }
        public Guid ID_pr { get; set; }
    }
}
