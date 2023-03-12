using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechFix.EntityModels;

namespace TechFix.TransportModels
{
    public class FixProductTransport
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ErrorDescription { get; set; }

        /// <summary>
        /// Tình trạng BH, Tình trạng sửa
        /// </summary>
        public string Condition { get; set; }

        /// <summary>
        /// Quy trình BH
        /// </summary>
        public string Process { get; set; }

        public int NumberOfTimes { get; set; }

        /// <summary>
        /// Warranty: SC, LK, LT, PC
        /// Repair: Chậm, thường, gấp
        /// </summary>
        public string Type { get; set; }
        public bool IsFixOrder { get; set; }

        public DateTime? EstimatedReturnDate { get; set; }
        public DateTime? FinishDate { get; set; }
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
        public Guid? FixStaffId { get; set; }
        public Guid? FixOrderId { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal TotalMoney { get; set; }
        public string ProductSerial { get; set; }
        public DateTime? WarrantyPeriod { get; set; }
        public bool IsCreatedBill { get; set; }
    }
}
