using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SOCAUD.Web.Startup))]
namespace SOCAUD.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
