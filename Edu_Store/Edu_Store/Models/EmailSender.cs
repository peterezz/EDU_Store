using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Net.Mail;

namespace Edu_Store.Models
{
    public class EmailSender : IEmailSender
    {
        private readonly ISendGridClient _sendgridClient;
        private readonly IOptions<SendGridSettings> sendGridSettings;

        public EmailSender(ISendGridClient sendgrid,IOptions<SendGridSettings> SendGridSettings)
        {
            this._sendgridClient = sendgrid;
            this.sendGridSettings = sendGridSettings;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var Fromemail = "mennamahrouse1211@gmail.com";
            var EmailName = "Store";
            var ApiKey = "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";

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(Fromemail, EmailName),
                Subject = subject,
                HtmlContent = htmlMessage
        };
            msg.AddTo(email);
            await _sendgridClient.SendEmailAsync(msg);

        }
    }
}
