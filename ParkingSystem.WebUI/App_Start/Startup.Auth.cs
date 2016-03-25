using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using ParkingSystem.WebUI.Identity;

namespace ParkingSystem.WebUI
{
    public partial class Startup
    {
        internal static IDataProtectionProvider DataProtectionProvider { get; private set; }
        
        public void ConfigureAuth(IAppBuilder app)
        {
            DataProtectionProvider = app.GetDataProtectionProvider();
            
            app.CreatePerOwinContext(() => DependencyResolver.Current.GetService<ApplicationUserManager>());

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}