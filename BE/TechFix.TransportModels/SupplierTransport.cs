using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechFix.TransportModels
{
    public class SupplierTransport
    {
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(11)]
        public string Phone { get; set; }
        [MaxLength(255)]
        public string Address { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public string Note { get; set; }
        public decimal AmountOwed { get; set; }
    }
}
