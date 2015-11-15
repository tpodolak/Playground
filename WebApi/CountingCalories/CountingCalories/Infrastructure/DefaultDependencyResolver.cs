using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http.Dependencies;
using Autofac;
using Autofac.Integration.WebApi;

namespace CountingKs.Infrastructure
{
    public class DefaultDependencyResolver : IDependencyResolver
    {
        private readonly AutofacWebApiDependencyResolver dependencyResolver;
        public DefaultDependencyResolver(params Assembly[] assemblies)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(assemblies);
            builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assemblies);
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