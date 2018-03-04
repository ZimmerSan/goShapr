using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GoSharpProject.Startup))]
namespace GoSharpProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
