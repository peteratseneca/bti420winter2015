using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Notes.Startup))]
namespace Notes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
