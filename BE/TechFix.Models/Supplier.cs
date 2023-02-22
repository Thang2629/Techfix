﻿using TechFix.EntityModels.Abstracts;

namespace TechFix.EntityModels
{
    public class Supplier : ProductAssociated
    {
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public decimal InDebt { get; set; }
        public string Note { get; set; }
        public string Code { get; set; }
        public string PhoneNumber { get; set; }
        public string ImagePath { get; set; }
    }
}