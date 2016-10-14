using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ValidationIssue.Startup))]
namespace ValidationIssue
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
