using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WholeSale.Web.Startup))]
namespace WholeSale.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
