using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TechFix.TransportModels.Dtos
{
    public class RepairProductByCustomerViewDto
    {
        public Guid? Id { get; set; }
        public string CustomerName { get; set; }
        public string FixOrderCode { get; set; }
        public int Count { get; set; }
        public decimal TotalMoney { get; set; }
        //public string SearchData { get; set; }
        public List<FixProductViewDto> FixProducts { get; set; }

    }

    public class FixProductViewDto
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ErrorDescription { get; set; }
        public string Condition { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public decimal TotalMoney { get; set; }
        public DateTime? EstimatedReturnDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Process { get; set; }
        public string CustomerName { get; set; }
        public string FixStaffName { get; set; }
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
        public string SearchData { get; set; }
    }
}
