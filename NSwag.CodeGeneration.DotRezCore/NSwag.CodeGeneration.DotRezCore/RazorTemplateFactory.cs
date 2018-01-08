using NJsonSchema.CodeGeneration;
using RazorLight;
using ITemplate = NJsonSchema.CodeGeneration.ITemplate;

namespace NSwag.CodeGeneration.DotRezCore
{
    public class RazorTemplateFactory : ITemplateFactory
    {
        private readonly IRazorLightEngine _razorEngineService;

        private readonly string _templateDirectory;

        public RazorTemplateFactory(string templateDirectory)
        {
            _templateDirectory = templateDirectory;
            _razorEngineService = EngineFactory.CreatePhysical(templateDirectory);
        }

        public ITemplate CreateTemplate(string language, string templateKey, object model)
        {
            return new RazorTemplate(_razorEngineService, $"{templateKey}.cshtml", model);
        }
    }
}