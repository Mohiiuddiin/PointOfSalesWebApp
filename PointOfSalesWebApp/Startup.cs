using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PointOfSalesWebApp.Startup))]
namespace PointOfSalesWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
