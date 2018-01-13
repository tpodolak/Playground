using System.Collections.Generic;
using DotRezCore.Api.Tests.Integration.ModelGenerator.Templating.Models;
using NJsonSchema;
using NSwag;
using NSwag.CodeGeneration;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.CSharp.Models;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.CodeGeneration
{
    internal class DotRezSwaggerToCSharpClientGenerator : SwaggerToCSharpClientGenerator
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
            return Settings.CSharpGeneratorSettings.TemplateFactory.CreateTemplate("CSharp", "Client",
                new DotRezClientTemplateModel(controllerName, controllerClassName, operations, null,
                    _document, Settings)).Render();
        }

        protected override string GenerateFile(string clientCode, IEnumerable<string> clientClasses,
            ClientGeneratorOutputType outputType)
        {
            return clientCode ?? string.Empty;
        }
    }
}