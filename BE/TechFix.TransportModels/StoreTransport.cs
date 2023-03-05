using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechFix.TransportModels
{
    public class StoreTransport
    {
        public string Name { get; set; }
        [MinLength(10), MaxLength(11)]
        public string Phone { get; set; }
        [MaxLength(255)]
        public string Address { get; set; }
    }
}
