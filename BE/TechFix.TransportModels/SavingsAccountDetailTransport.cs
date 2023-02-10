using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechFix.TransportModels
{
	public class SavingsAccountDetailTransport
	{
		public Guid? Id { get; set; }
		public string Name { get; set; }
		public string AccountNumber { get; set; }
		public int Amount { get; set; }
		public string InterestRateName { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public decimal InterestAmount { get; set; }
		public string Email { get; set; }
	}
}
