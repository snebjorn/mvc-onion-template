using Microsoft.AspNet.Identity;

namespace Web.Mail
{
    public interface IMailHandler
    {
        string GetMailMessage(IdentityMessage content);
    }
}
