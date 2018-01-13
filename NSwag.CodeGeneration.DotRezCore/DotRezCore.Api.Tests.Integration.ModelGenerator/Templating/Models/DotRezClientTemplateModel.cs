using System.Collections.Generic;
using NJsonSchema;
using NSwag;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.CSharp.Models;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.Templating.Models
{
    public class DotRezClientTemplateModel : CSharpClientTemplateModel
    {
        public SwaggerDocument Document { get; }
        public SwaggerToCSharpClientGeneratorSettings Settings { get; }

        public DotRezClientTemplateModel(string controllerName, string controllerClassName,
            IEnumerable<CSharpOperationModel> operations, JsonSchema4 exceptionSchema, SwaggerDocument document,
            SwaggerToCSharpClientGeneratorSettings settings)
            : base(controllerName, controllerClassName, operations, exceptionSchema, document, settings)
        {
            Document = document;
            Settings = settings;
        }

        public string Namespace => string.IsNullOrEmpty(Document.Info.Version)
            ? Settings.CSharpGeneratorSettings.Namespace
            : $"{Settings.CSharpGeneratorSettings.Namespace}.V{Document.Info.Version?.ToUpper()}";
    }
}