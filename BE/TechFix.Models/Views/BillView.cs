﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechFix.EntityModels.Views
{
    public class BillView
    {
        public Guid? Id { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? PaymentMethodId { get; set; }
        public Guid? SellerId { get; set; }
        public Guid? StoreId { get; set; }
        public string Note { get; set; }
        public decimal Vat { get; set; }
        public decimal TotalGoodsMoney { get; set; }
        public int TotalQuantity { get; set; }
        public decimal DiscountPerItem { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal AmountOwed { get; set; }
        public string Code { get; set; }
        public int Index { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string StoreName { get; set; }
        public DateTime SaleDate { get; set; }
        public string StaffName { get; set; }
    }
}