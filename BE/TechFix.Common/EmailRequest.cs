using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using TechFix.Common.AppSetting;

namespace TechFix.Common
{
    public class EmailRequest
    {
        public List<string> MailTo { get; set; }
        public List<string> MailCc { get; set; }
        public List<string> MailBcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }
        public bool UseTemplate { get; set; }

        public EmailRequest()
        {
            
        }

        public EmailRequest(string mailto, List<string> mailCc, List<string> mailBcc, string subject, string body, bool useTemplate = false) : this(new List<string> {mailto}, mailCc, mailBcc, subject, body, useTemplate)
        {
        }

        public EmailRequest(List<string> mailto, List<string> mailCc, List<string> mailBcc, string subject, string body, bool useTemplate = false)
        {
            this.MailTo = mailto;
            this.MailCc = mailCc;
            this.MailBcc = mailBcc;

            this.Subject = subject;
            this.Body = body;
            this.UseTemplate = useTemplate;
        }

    }
}

