
using System.Linq;
using System;
using System.Text;
using TechFix.Common.Constants;

namespace TechFix.Common
{
	public static class StringUtil
	{
		public static string HideEmail(string email)
		{
			var split = email.Split("@");
			email = email.Substring(0, email.Length > 5 ? 4 : 1);
			email += "*******@" + split[1];
			return email;
		}

        public static string CreateMd5(string input)
        {
            // Use input string to calculate MD5 hash
            using var md5 = System.Security.Cryptography.MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            var sb = new StringBuilder();
            foreach (var t in hashBytes)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString();
        }

        public static string GenerateWalletAddress(string walletType, int length = NumberConfig.WalletAddressMaxLength)
        {
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, length - 1)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray());
            return walletType switch
            {
                WalletType.VLGTokenAvailable => $"v{result}",
                WalletType.VLGCash => $"c{result}",
                WalletType.VLGPlus => $"p{result}",
                WalletType.Usdt => $"u{result}",
                WalletType.VmmToken => $"m{result}",
                _ => result
            };
        }

        public static string GetRootEmail(string email)
        {
            if (email == null)
                return null;
            if(email.Contains("+"))
            {
                var start = email.LastIndexOf("+", StringComparison.Ordinal);
                var end = email.IndexOf("@", start, StringComparison.Ordinal);
                if(start < end)
                {
                    var result = email.Remove(start, end - start);
                    return result;
                }
            }

            return email;
        }
    }
}
