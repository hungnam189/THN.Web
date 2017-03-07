using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(THN.Web.Startup))]
namespace THN.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
