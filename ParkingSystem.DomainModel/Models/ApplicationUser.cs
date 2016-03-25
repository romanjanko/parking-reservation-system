using Microsoft.AspNet.Identity.EntityFramework;

namespace ParkingSystem.DomainModel.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() 
            : base()
        {
        }
        
        public string UserNameWithoutEmailDomain
        {
            get
            {
                int index = UserName.IndexOf('@');

                if (index >= 0)
                {
                    return UserName.Substring(0, index).ToLowerInvariant();
                }
                else
                {
                    return UserName.ToLowerInvariant();
                }
            }
        }
    }
}
