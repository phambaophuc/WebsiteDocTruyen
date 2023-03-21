using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebsiteDocTruyen.Startup))]
namespace WebsiteDocTruyen
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
