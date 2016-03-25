using Microsoft.AspNet.Identity;
using ParkingSystem.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingSystem.WebUI.Identity
{
    public class ApplicationUserValidator : UserValidator<ApplicationUser>
    {
        private readonly List<string> _allowedEmailDomains = new List<string> { "apprise.com" };

        public ApplicationUserValidator(ApplicationUserManager applicationUserManager)
            : base(applicationUserManager)
        {
        }

        public override async Task<IdentityResult> ValidateAsync(ApplicationUser user)
        {
            IdentityResult result = await base.ValidateAsync(user);

            var emailDomain = user.Email.Split('@')[1];

            if (!_allowedEmailDomains.Contains(emailDomain.ToLower()))
            {
                var errors = result.Errors.ToList();

                errors.Add(String.Format("Email domain '{0}' is not allowed", emailDomain));

                result = new IdentityResult(errors);
            }

            return result;
        }
    }
}