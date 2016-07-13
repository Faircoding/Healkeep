using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Healkeep.Startup))]
namespace Healkeep
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
