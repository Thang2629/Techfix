using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechFix.TransportModels.Auth
{
	public class TwoFactorAuthTransport
	{
		public string ManualEntryKey { get; set; }
		public string QrCodeSetupImageUrl { get; set; }
	}
}
