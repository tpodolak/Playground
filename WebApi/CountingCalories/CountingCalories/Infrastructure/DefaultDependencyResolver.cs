﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Http.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using CountingCalories.Data;
using CountingCalories.Filters;

namespace CountingCalories.Infrastructure
{
    public class DefaultDependencyResolver : IDependencyResolver
    {
        private readonly AutofacWebApiDependencyResolver dependencyResolver;
        public DefaultDependencyResolver(params Assembly[] assemblies)
        {
            var builder = new ContainerBuilder();
            var httpConfiguration = GlobalConfiguration.Configuration;
            builder.RegisterInstance(GlobalConfiguration.Configuration);
            builder.RegisterHttpRequestMessage(httpConfiguration);
            builder.RegisterApiControllers(assemblies);
            builder.RegisterAssemblyTypes(assemblies)
                   .Except<ModelFactory>()
                   .Except<CustomHttpControllerSelector>()
                   .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assemblies)
                   .Except<ModelFactory>()
                   .Except<CustomHttpControllerSelector>();
            builder.Register<IModelFactory>(c => new ModelFactory(c.Resolve<ICountingCaloriesRepository>(), new UrlHelper(c.Resolve<HttpRequestMessage>()))).InstancePerRequest();
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