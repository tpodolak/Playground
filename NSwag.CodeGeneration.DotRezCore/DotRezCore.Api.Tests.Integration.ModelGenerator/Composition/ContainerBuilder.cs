using System;
using DotRezCore.Api.Tests.Integration.ModelGenerator.Composition.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.Composition
{
    public class ContainerBuilder : IContainerBuilder
    {
        private readonly IServiceCollection _serviceCollection = new ServiceCollection();

        public void RegisterModule(IModule module)
        {
            module.Register(this);
        }

        public void RegisterSingleton<TService, TImplementation>() where TService : class where TImplementation : class, TService
        {
            _serviceCollection.AddSingleton<TService, TImplementation>();
        }

        public void RegisterScoped<TService, TImplementation>() where TService : class where TImplementation : class, TService
        {
            _serviceCollection.AddScoped<TService, TImplementation>();
        }

        public void RegisterTransient<TService, TImplementation>() where TService : class where TImplementation : class, TService
        {
            _serviceCollection.AddTransient<TService, TImplementation>();
        }

        public IServiceProvider Build()
        {
            return _serviceCollection.BuildServiceProvider();
        }
    }
}