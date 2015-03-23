using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Family.Startup))]
namespace Family
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
