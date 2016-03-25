using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ParkingSystem.Core.Services;
using ParkingSystem.DomainModel.Models;

namespace ParkingSystem.WebUI.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) 
            : base(store) 
        {
            EmailService = new IdentityEmailService(new EmailService());

            UserValidator = new ApplicationUserValidator(this);

            var dataProtectionProvider = Startup.DataProtectionProvider;

            if (dataProtectionProvider != null)
            {
                this.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(
                        dataProtectionProvider.Create("ASP.NET Identity"));
            }
        }
    }
}