using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OSL_B2.Inventory.Web.Startup))]
namespace OSL_B2.Inventory.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
