using Microsoft.Owin;
using Owin;
using OwinCookiesAuthAdditionalData;

[assembly: OwinStartup(typeof(Startup))]
namespace OwinCookiesAuthAdditionalData
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
