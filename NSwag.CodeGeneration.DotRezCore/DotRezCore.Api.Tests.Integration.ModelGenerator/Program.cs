using System;
using System.IO;
using System.Threading.Tasks;
using DotRezCore.Api.Tests.Integration.ModelGenerator.CodeGeneration;
using DotRezCore.Api.Tests.Integration.ModelGenerator.Composition;
using Microsoft.Extensions.DependencyInjection;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IServiceProvider container = null;
            try
            {
                var containerBuilder = new ContainerBuilder();
                containerBuilder.RegisterModule(new BootstrapperModule());
                container = containerBuilder.Build();
                using (var scope = container.CreateScope())
                {
                    var integrationTestsRootPath =
                        Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../DotRezCore.Api.Tests.Integration"));
                    var application = scope.ServiceProvider.GetRequiredService<Application>();
                    var settings = new DotRezCodeGeneratorSettings
                    {
                        ClientFilesDestinationRoot = Path.Combine(integrationTestsRootPath, "ApiClients"),
                        ContractFilesDestinationRoot = Path.Combine(integrationTestsRootPath, "Models"),
                        CurrentContractNamespacePrefix = "DotRezCore.Api.Models",
                        DesiredClientNamespacePrefix = "DotRezCore.Api.Tests.Integration.ApiClients",
                        DesiredContractNamespacePrefix = "DotRezCore.Api.Tests.Integration.Models",
                        ClientName = "DotRezCoreApiClient"
                    };

                    await application.Generate(Api.Program.BuildWebHost(new[] {$"--{Constants.CommandLineArguments.UseCustomSwaggerSchema}", bool.TrueString}), settings);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                (container as IDisposable)?.Dispose();
                throw;
            }
        }
    }
}