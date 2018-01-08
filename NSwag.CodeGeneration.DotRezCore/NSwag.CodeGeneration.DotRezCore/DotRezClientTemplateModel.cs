using System.Collections.Generic;
using NJsonSchema;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.CSharp.Models;

namespace NSwag.CodeGeneration.DotRezCore
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
    }
}