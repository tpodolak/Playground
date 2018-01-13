using System;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.Composition.Interfaces
{
    public interface IContainerBuilder
    {
        void RegisterModule(IModule module);
        void RegisterSingleton<TService, TImplementation>() where TService : class where TImplementation : class, TService;
        void RegisterScoped<TService, TImplementation>() where TService : class where TImplementation : class, TService;
        void RegisterTransient<TService, TImplementation>() where TService : class where TImplementation : class, TService;
        IServiceProvider Build();
    }
}