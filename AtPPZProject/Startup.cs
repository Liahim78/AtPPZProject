using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AtPPZProject.Startup))]
namespace AtPPZProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
