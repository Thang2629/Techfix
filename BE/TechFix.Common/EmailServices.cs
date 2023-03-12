using System;

namespace TechFix.Common
{
    public class EmailServices
    {
        public void SendEmail(EmailModel email)
        {
            var errorMessage = EmailValidate(email);
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                throw new Exception(errorMessage);
            }
        }

        private string EmailValidate(EmailModel email)
        {
            if (string.IsNullOrEmpty(email.MailTo))
            {
                return "Missing MailTo";
            }
            if (string.IsNullOrEmpty(email.MailFrom))
            {
                return "Missing MailFrom";
            }
            if (string.IsNullOrEmpty(email.Subject))
            {
                return "Missing Subject";
            }
            if (string.IsNullOrEmpty(email.Body))
            {
                return "Missing Body";
            }
            return null;
        }
        public void SendEmail(string mailTo, string mailFrom, string mailCC, string mailBCC, string subject, string body)
        {
            SendEmail(new EmailModel
            {
                MailTo = mailTo,
                MailFrom = mailFrom,
                MailCC = mailCC,
                MailBCC = mailBCC,
                Subject = subject,
                Body = body
            });
        }

        public class EmailModel
        {
            public string MailTo { get; set; }
            public string MailFrom { get; set; }
            public string MailCC { get; set; }
            public string MailBCC { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            //public string BodyType { get; set; } //TEXT or HTML
        }
        
    }
}
