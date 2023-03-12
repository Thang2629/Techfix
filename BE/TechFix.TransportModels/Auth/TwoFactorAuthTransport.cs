namespace TechFix.TransportModels.Auth
{
	public class TwoFactorAuthTransport
	{
		public string ManualEntryKey { get; set; }
		public string QrCodeSetupImageUrl { get; set; }
	}
}
