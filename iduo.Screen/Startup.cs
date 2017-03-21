using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(iduo.Screen.Startup))]
namespace iduo.Screen
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
