using System;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Presentation.Web.App_Start
{
    public class EmailService : IIdentityMessageService
    {
        private readonly SmtpClient _smtpClient;

        public EmailService(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        public Task SendAsync(IdentityMessage message)
        {
            var mailMessage = new MailMessage()
            {
                Subject = message.Subject,
                IsBodyHtml = true,
                Body = message.Body
            };

            mailMessage.To.Add(message.Destination);

            // alternate view, for clients that can't view HTML
            var mimeType = new ContentType("text/html");
            var alternate = AlternateView.CreateAlternateViewFromString(message.Body, mimeType);
            mailMessage.AlternateViews.Add(alternate);

            try
            {
                return _smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception)
            {
                // Handle errors here.
                // For example someone typed in a wrong email address.
                // Right now this check is redundant, as it throws on the next line.
                throw;
            }
        }
    }
}
