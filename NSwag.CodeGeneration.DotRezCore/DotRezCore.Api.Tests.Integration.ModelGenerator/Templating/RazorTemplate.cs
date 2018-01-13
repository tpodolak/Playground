using RazorLight;
using ITemplate = NJsonSchema.CodeGeneration.ITemplate;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.Templating
{
    public class RazorTemplate : ITemplate
    {
        private readonly IRazorLightEngine _razorEngineService;
        private readonly object _model;
        private string _templateKey;

        public RazorTemplate(IRazorLightEngine razorEngineService, string templateKey, object model)
        {
            _razorEngineService = razorEngineService;
            _templateKey = templateKey;
            _model = model;
        }

        public string Render()
        {
            var parsed = _razorEngineService.Parse(_templateKey, _model);

            return parsed;
        }
    }
}