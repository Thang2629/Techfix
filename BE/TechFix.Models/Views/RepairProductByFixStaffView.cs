using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechFix.EntityModels.Views
{
    public class RepairProductByFixStaffView
    {
        public Guid? Id { get; set; }
        public string FixStaffName { get; set; }
        public Guid? FixProductId { get; set; }
        public string CustomerName { get; set; }
        public string FixOrderCode { get; set; }
        public string SearchData { get; set; }
        public string FixProductCode { get; set; }
        public string FixProductName { get; set; }
        public string FixProductErrorDescription { get; set; }
        public string FixProductCondition { get; set; }
        public DateTime? FixProductReceiptDate { get; set; }
        public decimal TotalMoney { get; set; }
        public DateTime? FixProductEstimatedReturnDate { get; set; }
        public DateTime? FixProductFinishDate { get; set; }
        public DateTime? FixProductReturnDate { get; set; }
        public string FixProductProcess { get; set; }
        public string Cpu { get; set; }
        public string Hdd { get; set; }
        public string Ram { get; set; }
        public string Wifi { get; set; }
        public string Pin { get; set; }
        public string Adapter { get; set; }
        public string Keyboard { get; set; }
        public string Psu { get; set; }
        public string Lcd { get; set; }
        public string Other { get; set; }
        public bool IsDeleted { get; set; }
    }
}
