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
        /// <summary>
        /// Giá vốn
        /// </summary>
        public decimal OriginalPrice { get; set; }
        /// <summary>
        /// Giá web
        /// </summary>
        public decimal WebPrice { get; set; }
        /// <summary>
        /// Giá nhập
        /// </summary>
        public decimal FakePrice { get; set; }
        public string Warranty { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Theo dõi tồn kho
        /// </summary>
        public bool IsInventoryTracking { get; set; }
        /// <summary>
        /// Cho phép bán âm
        /// </summary>
        public bool AllowNegativeSell { get; set; }
        public Guid? ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public Guid? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public Guid? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Guid? ProductUnitId { get; set; }
        /// <summary>
        /// Tên đơn vị tính
        /// </summary>
        public string ProductUnitName { get; set; }
        /// <summary>
        /// Tình trạng sản phẩm
        /// </summary>
        public Guid? ProductConditionId { get; set; }
        public string ProductConditionName { get; set;}
    }
}
