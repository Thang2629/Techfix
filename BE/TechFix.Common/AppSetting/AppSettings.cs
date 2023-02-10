using System;
using System.Collections.Generic;

namespace TechFix.Common.AppSetting
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string ImagePath { get; set; }
        public string EncryptionKey { get; set; }
        public string CurrentUrl { get; set; }
        public string VerifyApiAddress { get; set; }
        public string SiteKey { get; set; }
        public string SecretKey { get; set; }
        public string RecaptchaThreshold { get; set; }
        public bool IsCheckReCaptcha { get; set; }
        public decimal VLGPlusToUsd { get; set; }
        public string SenderEmail { get; set; }
        public string PassSenderEmail { get; set; }
        public bool IsDevelopment { get; set; }
        public string DefaultReferralUser { get; set; }
        public string SupporterEmail { get; set; }
        public string GoogleAuthenticationKey { get; set; }
        public string EmailAuthenticationKey { get; set; }
        public string ValidIps { get; set; }
        public string MailService { get; set; }
        public string MailGunKey { get; set; }
        public string SenderDisplayName { get; set; }
        public string AuthorizeNetEnvironment { get; set; }
        public string CreditCardApiLoginId { get; set; }
        public string CreditCardApiTransactionKey { get; set; }
        public DateTime StopFastStarBonusDate { get; set; }
        public string VioApi { get; set; }
        public string VioSecretkey { get; set; }
        public string VlinkApiUrl { get; set; }
        public DateTime StartDateUnlockToken { get; set; }
        public DateTime EndDateUnlockToken { get; set; }
        public DateTime StartRunCommission { get; set; }
        public string TransactionCode { get; set; }
        public DateTime EndMerchantConnection { get; set; }
        public List<string> AutoReportPackage { get; set; }
        public string FirstPreOrderStreamUrl { get; set; }
        public string SecondPreOrderStreamUrl { get; set; }
        public string TokenRateApi { get; set; }
        public decimal VNDRate { get; set; }
        public List<string> SpecialParents { get; set; }
        public DateTime AirdropBonusEndDate { get; set; }
        public int NumberBonusVmmToken { get; set; }
        public string VlinkMartApi { get; set; }
        public DateTime EndEventTicketDate { get; set; }
        public string VlinkExchangeApiUrl { get; set; }
    }
}
