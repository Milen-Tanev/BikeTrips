using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BikeTrips.Web.Startup))]
namespace BikeTrips.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
