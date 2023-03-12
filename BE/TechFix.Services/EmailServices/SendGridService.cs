using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using TechFix.Common;
using TechFix.Common.AppSetting;

namespace TechFix.Services.EmailServices
{
    public class SendGridService : IEmailService
    {
        private readonly ILogger<SendGridService> _logger;
        private readonly SendGridConfig _sendGridConfig;

        public SendGridService(ILogger<SendGridService> logger, IOptions<SendGridConfig> options)
        {
            _logger = logger;
            _sendGridConfig = options.Value;
        }

        public async Task SendAsync(EmailRequest emailRequest)
        {
            var apiKey = _sendGridConfig.SendGridApiKey;
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage { From = new EmailAddress(_sendGridConfig.SenderEmail, _sendGridConfig.SenderName), Subject = emailRequest.Subject, };
            foreach (var mailTo in emailRequest.MailTo)
            {
                msg.AddTo(new EmailAddress(mailTo));
            }
            if(emailRequest.MailCc?.Any() == true)
            {
                foreach (var cc in emailRequest.MailCc)
                {
                    msg.AddCc(new EmailAddress(cc));
                }
            }

            if(emailRequest.MailBcc?.Any() == true)
            {
                foreach (var bcc in emailRequest.MailBcc)
                {
                    msg.AddBcc(new EmailAddress(bcc));
                }
            }

            var body = emailRequest.Body;
            var isBodyHtml = emailRequest.IsHtml;

            if (emailRequest.UseTemplate)
            {
                var emailTemplate = GetEmailTemplate();
                body = emailTemplate.Replace("[BODY]", emailRequest.Body);
                isBodyHtml = true;
            }

            if (isBodyHtml)
            {
                msg.HtmlContent = body;
            }
            else
            {
                msg.PlainTextContent = body;
            }

            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.IsSuccessStatusCode ? $"Email to {string.Join(", ", emailRequest.MailTo)} queued successfully!" : "Something went wrong!");
        }

        private string GetEmailTemplate()
        {
            var emailTemplate = File.ReadAllText("./wwwroot/TemplateFile/EmailTemplate.html");
            return emailTemplate;
        }

    }
}
