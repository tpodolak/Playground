using System;
using System.IO;
using NJsonSchema.CodeGeneration;
using RazorLight;
using ITemplate = NJsonSchema.CodeGeneration.ITemplate;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.Templating
{
    public class RazorTemplateFactory : ITemplateFactory
    {
        private readonly IRazorLightEngine _razorEngineService;

        public RazorTemplateFactory()
        {
            var templatesLocation = Path.Combine(Environment.CurrentDirectory, "Templating", "Templates");
            _razorEngineService = EngineFactory.CreatePhysical(templatesLocation);
        }

        public ITemplate CreateTemplate(string language, string templateKey, object model)
        {
            return new RazorTemplate(_razorEngineService, $"{templateKey}.cshtml", model);
        }
    }
}