using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechFix.Common.Interfaces;
using TechFix.EntityModels;

namespace TechFix.TransportModels
{
    public class InputProductDto : IMapFrom<InputProduct>
    {
        #region FK Reference
        public string SupplierName { get; set; }
        public string StoreName { get; set; }
        public string InputUserName { get; set; }
        #endregion

        public DateTime? InputDate { get; set; }
        public string Note { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalGoodsMoney { get; set; }
        public decimal Discount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal AmountOwed { get; set; }
        public List<InputProductItemDto> Items { get; set; }

    }

    public class InputProductItemDto
    {
        public Guid? ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public string ProductCondition { get; set; }
        public string Warranty { get; set; }
        public int Quantity { get; set; }
        public string ProductUnit { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal TotalAmount 
        { 
            get
            {
                return Quantity * OriginalPrice;
            } 
        }
    }
}
