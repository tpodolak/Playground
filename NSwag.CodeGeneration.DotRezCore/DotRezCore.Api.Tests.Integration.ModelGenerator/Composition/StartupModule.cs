using System;
using System.IO;
using DotRezCore.Api.Tests.Integration.ModelGenerator.CodeGeneration;
using DotRezCore.Api.Tests.Integration.ModelGenerator.CodeGeneration.Interfaces;
using DotRezCore.Api.Tests.Integration.ModelGenerator.Composition.Interfaces;
using DotRezCore.Api.Tests.Integration.ModelGenerator.FileSystem;
using DotRezCore.Api.Tests.Integration.ModelGenerator.FileSystem.Interfaces;
using DotRezCore.Api.Tests.Integration.ModelGenerator.Swagger;
using DotRezCore.Api.Tests.Integration.ModelGenerator.Swagger.Interfaces;
using DotRezCore.Api.Tests.Integration.ModelGenerator.Templating;
using NJsonSchema.CodeGeneration;
using RazorLight;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.Composition
{
    public class StartupModule : IModule
    {
        public void Register(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterSingleton<Application, Application>();
            containerBuilder.RegisterSingleton<IRazorLightEngine, IRazorLightEngine>(provider =>
                EngineFactory.CreatePhysical(Path.Combine(Environment.CurrentDirectory, "Templating", "Templates")));
            containerBuilder.RegisterSingleton<ITemplateFactory, RazorTemplateFactory>();
            containerBuilder.RegisterScoped<IFileSystem, PhysicalFileSystem>();
            containerBuilder.RegisterScoped<ISchemaRetriever, SchemaRetriever>();
            containerBuilder.RegisterScoped<IDotRezClientGenerator, NSwagDotRezClientGenerator>();
            containerBuilder.RegisterScoped<IGeneratedCodeFileWriter, GeneratedCodeFileWriter>();
        }
    }
}