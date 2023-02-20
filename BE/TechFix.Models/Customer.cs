using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace TechFix.EntityModels
{
    public class Customer : BaseModel
    {
        public string ImagePath { get; set; }
        public string Team { get; set; } //Khách sỉ, Khách lẻ
        public string Code { get; set; }
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public DateTime? Birthday { get; set; }
        public string Note { get; set; }
    }
}
