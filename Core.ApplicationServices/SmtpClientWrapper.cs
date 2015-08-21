using System.Net.Mail;
using Core.ApplicationServices.Mail;

namespace Core.ApplicationServices
{
    /// <summary>
    /// A SmtpClient wrapper for testability.
    /// </summary>
    public class SmtpClientWrapper : SmtpClient, ISmtpClient
    {

    }
}