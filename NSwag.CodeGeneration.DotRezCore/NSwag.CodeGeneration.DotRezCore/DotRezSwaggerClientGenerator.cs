using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NJsonSchema.CodeGeneration.CSharp;
using NSwag.CodeGeneration.CSharp;

namespace NSwag.CodeGeneration.DotRezCore
{
    public class DotRezSwaggerClientGenerator
    {
        public async Task<GeneratedCode> Generate(string swaggerSpecification)
        {
            var swaggerDocument = await SwaggerDocument.FromJsonAsync(swaggerSpecification);

            var clientGenerator = CreateDotRezClientGenerator(swaggerDocument);
            var contractGenerator = CreateDotRezContractGenerator(swaggerDocument);

            try
            {
                string contractCode = contractGenerator.GenerateFile();
                var clientCode = clientGenerator.GenerateFile(ClientGeneratorOutputType.Full);
                
                return new GeneratedCode(
                    contractCode: contractCode,
                    clientCode: clientCode);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private DotRezSwaggerToCSharpClientGenerator CreateDotRezClientGenerator(SwaggerDocument swaggerDocument)
        {
            var settings = GetSwaggerCSharpGeneratorSettings();

            var clientGenerator = new DotRezSwaggerToCSharpClientGenerator(swaggerDocument, settings);
            return clientGenerator;
        }

        private DotRezCSharpGenerator CreateDotRezContractGenerator(SwaggerDocument swaggerSpecification)
        {
            var settings = GetSwaggerCSharpGeneratorSettings();

            var swaggerToCSharpTypeResolver = CreateSwaggerToCSharpTypeResolver(settings, swaggerSpecification);

            var cSharpGenerator = new DotRezCSharpGenerator(swaggerSpecification, settings.CSharpGeneratorSettings,
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

        private static SwaggerToCSharpClientGeneratorSettings GetSwaggerCSharpGeneratorSettings()
        {
            var fullPath = System.Reflection.Assembly.GetAssembly(typeof(DotRezSwaggerClientGenerator)).Location;

            var templateDirectory = Path.Combine(Path.GetDirectoryName(fullPath), "Templates");
            return new SwaggerToCSharpClientGeneratorSettings
            {
                ResponseArrayType = "System.Collections.Generic.List",
                ParameterNameGenerator = new DotRezParameterNameGenerator(),
                CSharpGeneratorSettings =
                {
                    ArrayType = "System.Collections.Generic.List",
                    GenerateDataAnnotations = false,
                    ArrayBaseType = "List",
                    TypeNameGenerator = new DotRezTypeNameGenerator(),
                    ClassStyle = CSharpClassStyle.Poco,
                    TemplateFactory = new RazorTemplateFactory(templateDirectory),
                }
            };
        }
    }
}