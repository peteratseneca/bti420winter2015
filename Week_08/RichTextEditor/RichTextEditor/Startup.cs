using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RichTextEditor.Startup))]
namespace RichTextEditor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
