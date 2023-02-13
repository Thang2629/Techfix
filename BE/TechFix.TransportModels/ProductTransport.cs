using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechFix.TransportModels
{
    public class ProductTransport
    {
        public string Code { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        public int Quantity { get; set; }

        public int MinimumNorm { get; set; }

        public int MaximumNorm { get; set; }

        public decimal OriginalCost { get; set; }  

        public decimal SellIn { get; set; }

        public decimal SellOut { get; set;}

        [MaxLength(100)]
        public string Warranty { get; set; }

        public string Description { get; set; }

        public bool IsInventoryTracking { get; set; }

        public bool AllowNegativeSell { get; set; }

        public bool IsDeleted { get; set; } 

        public Guid? ManufacturerId { get; set; }

        public Guid? SupplierId { get; set; }
        public Guid? StoreId { get; set; }

        public Guid? CategoryId { get; set; }

        public Guid? ProductConditionId { get; set; }

        public Guid? ProductUnitId { get; set; }
    }
}
