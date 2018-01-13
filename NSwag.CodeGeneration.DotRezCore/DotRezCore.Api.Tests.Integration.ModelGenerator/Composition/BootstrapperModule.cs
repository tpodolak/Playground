using DotRezCore.Api.Tests.Integration.ModelGenerator.Composition.Interfaces;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.Composition
{
    public class BootstrapperModule : IModule
    {
        public void Register(ContainerBuilder containerBuilder)
        {
            new StartupModule().Register(containerBuilder);
        }
    }
}