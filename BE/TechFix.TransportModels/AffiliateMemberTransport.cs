using System;
using Newtonsoft.Json;

namespace TechFix.TransportModels
{
	public class AffiliateMemberTransport
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Username { get; set; }
		public string Status { get; set; }
		//public string PhoneNumber { get; set; }
		public string Gender { get; set; }
		public string Address { get; set; }
		public DateTime? Birthday { get; set; }
		public string Country { get; set; }
		public string PackageName { get; set; }
		public string PackageLogo { get; set; }
		[JsonIgnore] public Guid? BiggestPackageId { get; set; }
		[JsonIgnore] public Guid? BusinessBiggestPackageId { get; set; }
		[JsonIgnore] public Guid? CustomerBiggestPackageId { get; set; }
		[JsonIgnore] public string Type { get; set; }
	}
}
