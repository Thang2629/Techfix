using Newtonsoft.Json;
using System;
using TechFix.Common.Interfaces;
using TechFix.EntityModels;

namespace TechFix.TransportModels.Dtos
{
    public class InventoryDto : IMapFrom<Product>
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal WebPrice { get; set; }
        public decimal FakePrice { get; set; }
        /// <summary>
        /// tổng giá trị nhập hàng
        /// </summary>
        public decimal TotalValue
        {
            get
            {
                return OriginalPrice * Quantity;
            }
            set
            {
                TotalValue = value;
            }
        }
        /// <summary>
        /// tổng giá trị tồn hàng theo giá Web
        /// </summary>
        public decimal StockValue
        {
            get
            {
                return WebPrice * Quantity;
            }
            set
            {
                StockValue = value;
            }
        }
        public Guid? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string ImagePath { get; set; }
    }
}
