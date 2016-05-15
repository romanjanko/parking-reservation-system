using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ParkingSystem.DomainModel.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() 
            : base()
        {
            UserAccountActive = true;
        }

        public bool UserAccountActive { get; set; }

        public string UserNameWithoutEmailDomain
        {
            get
            {
                var index = UserName.IndexOf('@');

                return index >= 0 ? UserName.Substring(0, index).ToLowerInvariant() : UserName.ToLowerInvariant();
            }
        }

        // TODO rework it for roles instead of hardcoded user name
        public bool IsUserAdmin() => string.Compare(UserName, "admin", StringComparison.Ordinal) == 0;
    }
}
