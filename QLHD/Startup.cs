using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QLHD.Startup))]
namespace QLHD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
