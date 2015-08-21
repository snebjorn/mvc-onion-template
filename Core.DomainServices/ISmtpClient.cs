using System.Net.Mail;

namespace Core.ApplicationServices.Mail
{
    /// <summary>
    /// Interface used for testing.
    /// </summary>
    public interface ISmtpClient
    {
        void Send(MailMessage msg);
    }
}
