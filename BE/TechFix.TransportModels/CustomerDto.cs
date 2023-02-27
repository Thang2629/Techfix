using System;
using TechFix.Common.Interfaces;
using TechFix.EntityModels;

namespace TechFix.TransportModels
{
    public class CustomerDto : IMapFrom<Customer>, IMapTo<Customer>
    {
        public string ImagePath { get; set; }
        public string Team { get; set; } //Khách sỉ, Khách lẻ
        public string Code { get; set; }
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public DateTime? Birthday { get; set; }
        public string Note { get; set; }
        public decimal InDebtAmount { get; set; }
    }
}
