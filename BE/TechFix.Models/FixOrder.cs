using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechFix.EntityModels
{
    public class FixOrder : BaseModel
    {
        public Guid? CustomerId { get; set; }

        public Customer Customer{ get; set; }

        public Guid? StoreId { get; set; }

        public Store Store { get; set; }
        
        public Guid? CashierId { get; set; }

        public User Cashier { get; set; }

        public string Code { get; set; }

        public string Note { get; set; }

        public DateTime ReceivedDate { get; set; }

        public bool IsFixOrder { get; set; }

        public List<FixProduct> FixProducts { get; set; }
    }
}
