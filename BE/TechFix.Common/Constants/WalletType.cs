using System.Collections.Generic;

namespace TechFix.Common.Constants
{
    public class WalletType
    {
        public const string VLGCoin = "VLGCOIN";
        public const string VLGPlus = "VLGPLUS";// VTP
        public const string VLGShare = "VLGSHARE";
        public const string VLGToken = "VLGTOKEN";//VLG Token Matching
        public const string VLGCash = "VLGCASH";// VUSD
        public const string VLGTokenBlock = "VLGTOKENBLOCK";
        public const string VLGTokenAvailable = "VLGTOKENAVAILABLE";// VLG Token
        public const string VLGAffiliatesPoint = "VLGAFFILIATESPOINT";
        public const string VLGLifetimePoint = "VLGLIFETIMEPOINT";
        public const string MoongleWallet = "MOONGLEWALLET";
        public const string Usdt = "USDT";
        public const string Btc = "BTC";
        public const string Eth = "ETH";
        public const string Doge = "DOGE";
        public const string Shiba = "SHIBA";
        public const string VtpBonus = "VTPBONUS";
        public const string VLGGold = "VLGGOLD";
        public const string ShareAp = "SHAREAP";
        public const string L1Wallet = "L1WALLET";
        public const string Eb3Wallet = "EB3WALLET";
        public const string Eb5Wallet = "EB5WALLET";
        public const string Airdrop = "AIRDROP";
        public const string AirdropBlock = "AIRDROPBLOCK";
        public const string VmmToken = "VMMTOKEN";
        public const string VmmStaking = "VMMSTAKING";
        public const string VmmBonus = "VMMBONUS";
        public const string VmmBurn = "VMMBURN";
        public const string VmmBlock = "VMMBLOCK";
        public static List<string> LimitCheckoutTypes => new() { VLGCash, VLGPlus, VLGGold, Usdt };
        public static List<string> LimitKycCheckoutTypes => new() { VLGCash, VLGPlus, Usdt };
    }
}