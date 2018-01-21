using ITemplate = NJsonSchema.CodeGeneration.ITemplate;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.Templating
{
    public class PreRenderedTemplate : ITemplate
    {
        private readonly string _renderedTemplate;

        public PreRenderedTemplate(string renderedTemplate)
        {
            _renderedTemplate = renderedTemplate;
        }

        public string Render()
        {
            return _renderedTemplate;
        }
    }
}