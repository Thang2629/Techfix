using System;

namespace TechFix.TransportModels
{
	public class SavingsAccountTransport
	{
		public Guid? Id { get; set; }
		public string Name { get; set; }
		public string AccountNumber { get; set; }
		public int Amount { get; set; }
		public string InterestRateName { get; set; }
		public string Username { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public decimal OFSPrice { get; set; }
		public decimal Interest { get; set; }
		public string Status { get; set; }
	}
}
