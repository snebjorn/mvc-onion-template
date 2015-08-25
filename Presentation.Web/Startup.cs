using Microsoft.Owin;
using Owin;
using Presentation.Web;

[assembly: OwinStartup(typeof(Startup))]
namespace Presentation.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
