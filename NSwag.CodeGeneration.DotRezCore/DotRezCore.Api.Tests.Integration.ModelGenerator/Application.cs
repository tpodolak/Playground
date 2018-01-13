using System.Threading.Tasks;
using DotRezCore.Api.Tests.Integration.ModelGenerator.CodeGeneration;
using DotRezCore.Api.Tests.Integration.ModelGenerator.CodeGeneration.Interfaces;
using DotRezCore.Api.Tests.Integration.ModelGenerator.FileSystem.Interfaces;
using DotRezCore.Api.Tests.Integration.ModelGenerator.Swagger.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator
{
    public class Application
    {
        private readonly IDotRezClientGenerator _dotRezClientGenerator;
        private readonly IGeneratedCodeFileWriter _generatedCodeFileWriter;
        private readonly ISchemaRetriever _schemaRetriever;

        public Application(IDotRezClientGenerator dotRezClientGenerator,
            IGeneratedCodeFileWriter generatedCodeFileWriter,
            ISchemaRetriever schemaRetriever)
        {
            _dotRezClientGenerator = dotRezClientGenerator;
            _generatedCodeFileWriter = generatedCodeFileWriter;
            _schemaRetriever = schemaRetriever;
        }

        public async Task Generate(IWebHost host, DotRezCodeGeneratorSettings settings)
        {
            foreach (var swaggerSchema in _schemaRetriever.RetrieveSchemas(host))
            {
                var generatedCode = await _dotRezClientGenerator.Generate(swaggerSchema, settings);
                await _generatedCodeFileWriter.WriteAsync(generatedCode, settings);
            }
        }
    }
}