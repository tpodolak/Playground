using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CountingCalories.Data;
using CountingCalories.Infrastructure.DependencyInjection;
using CountingCalories.Infrastructure.Routing;

namespace CountingCalories
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var httpConfiguration = GlobalConfiguration.Configuration;
            ConfigureDependencyResolver();
            var service = (IRouteVersionFinder)(httpConfiguration.DependencyResolver).GetService(typeof(IRouteVersionFinder));

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(configuration => WebApiConfig.Register(configuration, service));
            WebApiConfig.ConfigureTracers(httpConfiguration);
            WebApiConfig.ConfigureHttps(httpConfiguration);
            WebApiConfig.ConfigureFormatters(httpConfiguration);
            WebApiConfig.ConfigureETagSupport(httpConfiguration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        private void ConfigureDependencyResolver()
        {
            var callingAssembly = Assembly.GetCallingAssembly();
            var dataAssembly = Assembly.GetAssembly(typeof(ICountingCaloriesRepository));
            GlobalConfiguration.Configuration.DependencyResolver = new DefaultDependencyResolver(callingAssembly, dataAssembly);
        }
    }
}