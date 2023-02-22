using Newtonsoft.Json;
using System;
using TechFix.Common.Interfaces;
using TechFix.EntityModels;

namespace TechFix.TransportModels.Dtos
{
    public class ProductDto : IMapFrom<Product>
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int MinimumNorm { get; set; }
        public int MaximumNorm { get; set; }
        public decimal OriginalCost { get; set; }
        public decimal SellIn { get; set; }
        public decimal SellOut { get; set; }
        public string Warranty { get; set; }
        public string Description { get; set; }
        public bool IsInventoryTracking { get; set; }
        public bool AllowNegativeSell { get; set; }
    }
}
