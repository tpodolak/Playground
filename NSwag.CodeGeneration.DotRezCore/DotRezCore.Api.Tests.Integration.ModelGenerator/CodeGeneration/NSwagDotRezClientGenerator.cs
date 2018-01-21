using System.Threading.Tasks;
using DotRezCore.Api.Tests.Integration.ModelGenerator.CodeGeneration.Interfaces;
using NJsonSchema.CodeGeneration;
using NJsonSchema.CodeGeneration.CSharp;
using NSwag;
using NSwag.CodeGeneration;
using NSwag.CodeGeneration.CSharp;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.CodeGeneration
{
    public class NSwagDotRezClientGenerator : IDotRezClientGenerator
    {
        private readonly ITemplateFactory _templateFactory;

        public NSwagDotRezClientGenerator(ITemplateFactory templateFactory)
        {
            _templateFactory = templateFactory;
        }

        public async Task<GeneratedCode> Generate(string swaggerSpecification, DotRezCodeGeneratorSettings settings)
        {
            var swaggerDocument = await SwaggerDocument.FromJsonAsync(swaggerSpecification);

            var clientGenerator = CreateDotRezClientGenerator(swaggerDocument, settings);
            var contractGenerator = CreateDotRezContractGenerator(swaggerDocument, settings);
            var contractCode = contractGenerator.GenerateFile();
            var clientCode = clientGenerator.GenerateFile(ClientGeneratorOutputType.Full);

            return new GeneratedCode(
                contractCode: contractCode,
                clientCode: clientCode);
        }

        private DotRezSwaggerToCSharpClientGenerator CreateDotRezClientGenerator(SwaggerDocument swaggerDocument,
            DotRezCodeGeneratorSettings settings)
        {
            var nswagSettings = GetSwaggerCSharpGeneratorSettings(settings);

            var clientGenerator = new DotRezSwaggerToCSharpClientGenerator(swaggerDocument, nswagSettings);
            return clientGenerator;
        }

        private DotRezCSharpGenerator CreateDotRezContractGenerator(SwaggerDocument swaggerSpecification, DotRezCodeGeneratorSettings settings)
        {
            var nswagSettings = GetSwaggerCSharpGeneratorSettings(settings);

            var swaggerToCSharpTypeResolver = CreateSwaggerToCSharpTypeResolver(nswagSettings, swaggerSpecification);

            var cSharpGenerator = new DotRezCSharpGenerator(swaggerSpecification, nswagSettings.CSharpGeneratorSettings,
                swaggerToCSharpTypeResolver);

            return cSharpGenerator;
        }

        private SwaggerToCSharpTypeResolver CreateSwaggerToCSharpTypeResolver(
            SwaggerToCSharpClientGeneratorSettings settings, SwaggerDocument parsedDocument)
        {
            var swaggerToCSharpTypeResolver =
                SwaggerToCSharpTypeResolver.CreateWithDefinitions(settings.CSharpGeneratorSettings, parsedDocument);
            return swaggerToCSharpTypeResolver;
        }

        private SwaggerToCSharpClientGeneratorSettings GetSwaggerCSharpGeneratorSettings(DotRezCodeGeneratorSettings settings)
        {
            return new SwaggerToCSharpClientGeneratorSettings
            {
                ResponseArrayType = "System.Collections.Generic.List",
                ParameterNameGenerator = new DotRezParameterNameGenerator(),
                CSharpGeneratorSettings =
                {
                    ArrayType = "System.Collections.Generic.List",
                    GenerateDataAnnotations = false,
                    ArrayBaseType = "List",
                    TypeNameGenerator = new DotRezTypeNameGenerator(settings),
                    ClassStyle = CSharpClassStyle.Poco,
                    TemplateFactory = _templateFactory,
                    Namespace = settings.DesiredClientNamespacePrefix
                },
                ClassName = settings.ClientName
            };
        }
    }
}