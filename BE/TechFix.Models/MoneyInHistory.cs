using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TechFix.EntityModels
{
    public class MoneyInHistory : BaseModel
    {
        #region FK

        public Guid? BillId { get; set; }
        public Bill Bill { get; set; }

        public User Cashier { get; set; }
        public Guid? CashierId { get; set; }

        public Guid? PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public Guid? CustomerId { get; set; }
        public Customer Customer { get; set; }

        #endregion

        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
    }
}
