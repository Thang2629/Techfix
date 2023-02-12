using System;
using System.Text;
using System.Net.Mail;

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

        public static bool IsValidEmail(string emailAddress)
        {
            try
            {
                var m = new MailAddress(emailAddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
