using System;

namespace TechFix.EntityModels
{
    public class ImportHistory : BaseModel
    {
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public Guid? UserId { get; set; }
        public User User { get; set; }
    }
}
