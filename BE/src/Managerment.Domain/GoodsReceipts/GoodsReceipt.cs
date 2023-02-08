using Managerment.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Managerment.GoodsReceipts
{
    public class GoodsReceipt : AuditedAggregateRoot<Guid>
    {
        public string Receipt_code { get; set; }
        public DateTime Receipt_date { get; set; }
        public string Notes { get; set; }
        public EPayment Payment_method { get; set; }
        public decimal Total_price { get; set; }
        public decimal Discount { get; set; }
        public decimal Total_money { get; set; }
        public decimal Paid { get; set; }
        public decimal Debt { get; set; }
        public bool Can_return { get; set; }
        public bool Is_delete { get; set; }
        public Guid Id_store { get; set; }
        public Guid Id_user { get; set; }
        public Guid Id_supplier { get; set; }
        private GoodsReceipt()
        {
            /* This constructor is for deserialization / ORM purpose */
        }
        internal GoodsReceipt(
               Guid id,
               string receipt_code,
               DateTime receipt_date,
               decimal total_price,
               decimal discount,
               decimal total_money,
               decimal paid,
               decimal debt,
               string notes,
               EPayment payment_method,
               bool can_return,
               Guid id_store,
               Guid id_user,
               Guid id_supplier
           )
           : base(id)
        {
            Receipt_code = receipt_code;
            Receipt_date = receipt_date;
            Total_price = total_price;
            Discount = discount;
            Total_money = total_money;
            Paid = paid;
            Debt = debt;
            Notes = notes;
            Payment_method = payment_method;
            Can_return = can_return;
            Id_store = id_store;
            Id_user = id_user;
            Id_supplier = id_supplier;
        }
    }
}
