using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CountingCalories.Data;
using CountingCalories.Infrastructure;

namespace CountingCalories
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            WebApiConfig.ConfigureHttps(GlobalConfiguration.Configuration);
            WebApiConfig.ConfigureFormatters(GlobalConfiguration.Configuration);
            WebApiConfig.ConfigureETagSupport(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            ConfigureDependencyResolver();
        }

        private void ConfigureDependencyResolver()
        {
            var callingAssembly = Assembly.GetCallingAssembly();
            var dataAssembly = Assembly.GetAssembly(typeof(ICountingCaloriesRepository));
            GlobalConfiguration.Configuration.DependencyResolver = new DefaultDependencyResolver(callingAssembly, dataAssembly);
        }
    }
}