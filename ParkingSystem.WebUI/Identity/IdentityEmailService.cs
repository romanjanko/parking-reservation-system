using Microsoft.AspNet.Identity;
using ParkingSystem.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace ParkingSystem.WebUI.Identity
{
    public class IdentityEmailService : IIdentityMessageService
    {
        private readonly IEmailService _emailService;

        public IdentityEmailService(IEmailService emailService)
        {
            _emailService = emailService;
        }
        
        public Task SendAsync(IdentityMessage message)
        {
            return _emailService.SendAsync(message.Destination, message.Subject, message.Body);
        }
    }
}