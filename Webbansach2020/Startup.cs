using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Webbansach2020.Startup))]
namespace Webbansach2020
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
