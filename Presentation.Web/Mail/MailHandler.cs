using System;
using System.IO;
using RazorEngine.Templating;

namespace Presentation.Web.Mail
{
    public class MailHandler : IMailHandler
    {
        private readonly IRazorEngineService _engineService;

        public MailHandler(
            IRazorEngineService engineService)
        {
            _engineService = engineService;
        }

        public string GetMailMessage<T>(T model, string emailTemplateName)
        {
            var templateFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MailTemplates");
            var mailTemplate = Path.Combine(templateFolderPath, emailTemplateName);
            var template = File.ReadAllText(mailTemplate);

            var emailHtmlBody = _engineService.RunCompile(template, "Mail", typeof(T), model);

            return emailHtmlBody;
        }
    }
}
