using NJsonSchema.CodeGeneration;
using RazorLight;
using ITemplate = NJsonSchema.CodeGeneration.ITemplate;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.Templating
{
    public class RazorTemplateFactory : ITemplateFactory
    {
        private readonly IRazorLightEngine _razorLightEngine;

        public RazorTemplateFactory(IRazorLightEngine razorLightEngine)
        {
            _razorLightEngine = razorLightEngine;
        }

        public ITemplate CreateTemplate(string language, string templateKey, object model)
        {
            var renderedTemplate = _razorLightEngine.Parse($"{templateKey}.cshtml", model);

            return new PreRenderedTemplate(renderedTemplate);
        }
    }
}