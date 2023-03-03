using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechFix.TransportModels.Dtos
{
    public class GetRepairProductReportViewDto
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string CustomerName { get; set; }
        public string StoreName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string FixStaffName { get; set; }
        public int? TotalQuantity { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? AmountPaid { get; set; }
        public decimal? AmountOwed { get; set; }
        public bool? IsDeleted { get; set; }
        public List<FixProductReportDto> FixProducts { get; set; }
    }

    public class FixProductReportDto
    {
        public Guid? FixProductId { get; set; }
        public string FixProductCode { get; set; }
        public string FixProductName { get; set; }
        public string ProductSerial { get; set; }
        public string Condition { get; set; }
        public DateTime? WarrantyPeriod { get; set; }
        public decimal? TotalMoney { get; set; }
        public string FixStaffName { get; set; }
        public string FixProductUnitName { get; set; }
        public int FixProductQuantity { get; set; }
    }
}
