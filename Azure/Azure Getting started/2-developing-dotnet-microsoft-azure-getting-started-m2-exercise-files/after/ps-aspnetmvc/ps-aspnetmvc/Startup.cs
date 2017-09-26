using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ps_aspnetmvc.Startup))]
namespace ps_aspnetmvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
