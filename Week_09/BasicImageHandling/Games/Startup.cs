using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Games.Startup))]
namespace Games
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
