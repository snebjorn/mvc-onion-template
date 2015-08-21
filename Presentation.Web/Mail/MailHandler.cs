using System;
using System.IO;
using Core.ApplicationServices.Mail;
using Microsoft.AspNet.Identity;
using RazorEngine.Templating;

namespace Web.Mail
{
    public class MailHandler : IMailHandler
    {
        private readonly IRazorEngineService _engineService;
        
        public MailHandler(
            IRazorEngineService engineService)
        {
            _engineService = engineService;
        }

        public string GetMailMessage(IdentityMessage content)
        {
            var templateFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MailTemplates");
            var mailTemplate = Path.Combine(templateFolderPath, "EmailTemplate.cshtml");
            var template = File.ReadAllText(mailTemplate);
            
            var model = new EmailModel
            {
                Subject = content.Subject,
                Content = content.Body
            };

            var emailHtmlBody = _engineService.RunCompile(template, "Mail", typeof(EmailModel), model);

            return emailHtmlBody;
        }
    }
}
