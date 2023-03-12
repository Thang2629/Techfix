using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechFix.Common.Interfaces;
using TechFix.EntityModels;

namespace TechFix.TransportModels.Dtos
{
    public class ProductHistoryDto : IMapFrom<ProductHistory>
    {
        public Guid? Id { get; set; }
        public DateTime DateTime { get; set; }
        public string UserName { get; set; }
        public string Action { get; set; }
        public string Code { get; set; }
        public int Quantity { get; set; }
        public decimal OriginalPrice { get; set; }
        public string ProductCondition { get; set; }
        public string Warranty { get; set; }
        public string StoreName { get; set; }
    }
}
