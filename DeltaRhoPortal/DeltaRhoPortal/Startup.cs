using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DeltaRhoPortal.Startup))]
namespace DeltaRhoPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
