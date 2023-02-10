using Managerment.ProductWarranties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Managerment.OrderWarranties
{
    public class CreateOrderWarrantyDto
    {
        public string Text { get; set; }
        public Guid ID_cus { get; set; }
        public Guid ID_user { get; set; }
        public List<ListProductWarrantyDto> Product_warranties { get; set; }
    }
}
