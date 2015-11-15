using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Http.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using CountingKs.Data;

namespace CountingKs.Infrastructure
{
    public class DefaultDependencyResolver : IDependencyResolver
    {
        private readonly AutofacWebApiDependencyResolver dependencyResolver;
        public DefaultDependencyResolver(params Assembly[] assemblies)
        {
            //            var builder = new ContainerBuilder();
            //            builder.RegisterHttpRequestMessage(GlobalConfiguration.Configuration);
            //            builder.RegisterApiControllers(assemblies);
            //            builder.RegisterAssemblyTypes().Except<ModelFactory>().AsImplementedInterfaces();
            //            builder.RegisterAssemblyTypes(assemblies).Except<ModelFactory>();
            //            // builder.Register<IModelFactory>(c => new ModelFactory(new UrlHelper(c.Resolve<HttpRequestMessage>()))).InstancePerRequest();
            //            var container = builder.Build();

            var builder = new ContainerBuilder();
            builder.RegisterHttpRequestMessage(GlobalConfiguration.Configuration);
            builder.RegisterApiControllers(assemblies);
            builder.RegisterAssemblyTypes(assemblies).Except<ModelFactory>().AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assemblies).Except<ModelFactory>();
            builder.Register<IModelFactory>(c => new ModelFactory(c.Resolve<ICountingKsRepository>(), new UrlHelper(c.Resolve<HttpRequestMessage>()))).InstancePerRequest();
            var container = builder.Build();
            dependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
        public void Dispose()
        {
            dependencyResolver.Dispose();
        }

        public object GetService(Type serviceType)
        {
            return dependencyResolver.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return dependencyResolver.GetServices(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            return dependencyResolver.BeginScope();
        }
    }
}