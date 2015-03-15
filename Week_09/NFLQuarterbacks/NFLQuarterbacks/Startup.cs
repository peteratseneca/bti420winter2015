using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NFLQuarterbacks.Startup))]
namespace NFLQuarterbacks
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
