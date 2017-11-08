using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebStoreProject.Startup))]
namespace WebStoreProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
