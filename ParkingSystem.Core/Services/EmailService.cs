using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace ParkingSystem.Core.Services
{
    public interface IEmailService
    {
        Task SendAsync(string sentTo, string subject, string body);
    }

    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;

        public EmailService()
        {
            _smtpClient = new SmtpClient();
        }

        public Task SendAsync(string sentTo, string subject, string body)
        {
            MailMessage mailMessage = new MailMessage();

            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(new MailAddress(sentTo));
            mailMessage.Subject = subject;
            mailMessage.Body = body;

            return _smtpClient.SendMailAsync(mailMessage);
        }
    }
}
