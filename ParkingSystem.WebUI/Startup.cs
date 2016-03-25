using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ParkingSystem.WebUI.Startup))]

namespace ParkingSystem.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}