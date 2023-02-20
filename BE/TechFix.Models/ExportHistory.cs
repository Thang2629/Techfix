using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TechFix.EntityModels
{
    public class ExportHistory : BaseModel
    {
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public Guid? UserId { get; set; }
        public User User { get; set; }
    }
}
