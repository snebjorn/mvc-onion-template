namespace Presentation.Web.Mail
{
    public interface IMailHandler
    {
        string GetMailMessage<T>(T content, string template);
    }
}
