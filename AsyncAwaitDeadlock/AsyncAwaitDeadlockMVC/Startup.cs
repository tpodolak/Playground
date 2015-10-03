using AsyncAwaitDeadlock;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace AsyncAwaitDeadlock
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
