using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ebuy.Startup))]
namespace Ebuy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
