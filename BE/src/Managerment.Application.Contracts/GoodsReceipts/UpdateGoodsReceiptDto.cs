using Managerment.Payments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Managerment.GoodsReceipts
{
    public class UpdateGoodsReceiptDto
    {   
        public Guid Id { get; set; }
        public List<GoodsReceiptProductDto> Product_receipt { get; set; }
        public DateTime Receipt_date { get; set; }
        public decimal Total_price { get; set; }
        public decimal Discount { get; set; }
        public decimal Total_money { get; set; }
        public decimal Paid { get; set; }
        public decimal Debt { get; set; }
        public string Notes { get; set; }
        public EPayment Payment_method { get; set; }
        public bool Can_return { get; set; }
        public bool Is_delete { get; set; }
        public Guid Id_store { get; set; }
        public Guid Id_user { get; set; }
        public Guid Id_supplier { get; set; }
    }
}
