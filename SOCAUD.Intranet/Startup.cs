using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SOCAUD.Intranet.Startup))]
namespace SOCAUD.Intranet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
