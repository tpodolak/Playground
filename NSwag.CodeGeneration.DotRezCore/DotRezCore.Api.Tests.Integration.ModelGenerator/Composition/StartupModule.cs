using DotRezCore.Api.Tests.Integration.ModelGenerator.CodeGeneration;
using DotRezCore.Api.Tests.Integration.ModelGenerator.CodeGeneration.Interfaces;
using DotRezCore.Api.Tests.Integration.ModelGenerator.Composition.Interfaces;
using DotRezCore.Api.Tests.Integration.ModelGenerator.FileSystem;
using DotRezCore.Api.Tests.Integration.ModelGenerator.FileSystem.Interfaces;
using DotRezCore.Api.Tests.Integration.ModelGenerator.Swagger;
using DotRezCore.Api.Tests.Integration.ModelGenerator.Swagger.Interfaces;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.Composition
{
    public class StartupModule : IModule
    {
        public void Register(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterSingleton<Application, Application>();
            containerBuilder.RegisterScoped<IFileSystem, PhysicalFileSystem>();
            containerBuilder.RegisterScoped<ISchemaRetriever, SchemaRetriever>();
            containerBuilder.RegisterScoped<IDotRezClientGenerator, NSwagDotRezClientGenerator>();
            containerBuilder.RegisterScoped<IGeneratedCodeFileWriter, GeneratedCodeFileWriter>();
        }
    }
}