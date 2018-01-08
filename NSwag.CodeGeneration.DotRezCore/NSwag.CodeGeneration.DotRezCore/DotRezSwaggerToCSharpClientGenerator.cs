using System.Collections.Generic;
using System.Net.Http;
using NJsonSchema;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.CSharp.Models;

namespace NSwag.CodeGeneration.DotRezCore
{
    public class DotRezSwaggerToCSharpClientGenerator : SwaggerToCSharpClientGenerator
    {
        private readonly SwaggerDocument _document;

        public DotRezSwaggerToCSharpClientGenerator(SwaggerDocument document,
            SwaggerToCSharpClientGeneratorSettings settings) : base(document, settings)
        {
            _document = document;
        }

        protected override string GenerateClientClass(string controllerName, string controllerClassName,
            IList<CSharpOperationModel> operations,
            ClientGeneratorOutputType outputType)
        {
            SwaggerToCSharpTypeResolver resolver = this.Resolver as SwaggerToCSharpTypeResolver;
            JsonSchema4 exceptionSchema = resolver != null ? resolver.ExceptionSchema : null;
            return this.Settings.CSharpGeneratorSettings.TemplateFactory.CreateTemplate("CSharp", "Client",
                new DotRezClientTemplateModel(controllerName, controllerClassName, operations, exceptionSchema,
                    _document, Settings)
                {
                    GenerateContracts = (outputType == ClientGeneratorOutputType.Full ||
                                         outputType == ClientGeneratorOutputType.Contracts),
                    GenerateImplementation = (outputType == ClientGeneratorOutputType.Full ||
                                              outputType == ClientGeneratorOutputType.Implementation)
                }).Render();
        }

        protected override string GenerateFile(string clientCode, IEnumerable<string> clientClasses,
            ClientGeneratorOutputType outputType)
        {
            return clientCode ?? string.Empty;
        }
    }
}