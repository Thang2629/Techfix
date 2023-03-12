using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechFix.TransportModels.Dtos
{
    public class CalculateTotalFundDto
    {
        public decimal PositiveFund { get; set; } = 0;
        public decimal NegativeFund { get; set; } = 0;
        public decimal TotalFund { get {
                return PositiveFund - NegativeFund;
            } }
    }
}
