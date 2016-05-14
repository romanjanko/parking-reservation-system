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

        public IdentityResult MakeUserActive(ApplicationUser user)
        {
            //TODO - implement it in better way in the future
            user.EmailConfirmed = true;
            
            return UpdateAsync(user).Result;
        }

        public IdentityResult MakeUserInactive(ApplicationUser user)
        {
            //TODO - implement it in better way in the future
            //Also, if the user still has a confirmation email with
            //valid token (should expire within 24hours?), then he can confirm it once again and log in.
            user.EmailConfirmed = false;

            return UpdateAsync(user).Result;
        }
    }
}