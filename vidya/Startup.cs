using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(vidya.Startup))]
namespace vidya
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
