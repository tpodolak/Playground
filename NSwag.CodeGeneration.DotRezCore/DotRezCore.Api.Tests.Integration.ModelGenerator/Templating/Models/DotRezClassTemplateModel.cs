using System.Linq;
using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;
using NJsonSchema.CodeGeneration.CSharp.Models;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.Templating.Models
{
    public class DotRezClassTemplateModel : ClassTemplateModel
    {
        private readonly string _typeName;

        public DotRezClassTemplateModel(string typeName, CSharpGeneratorSettings settings,
            CSharpTypeResolver resolver, JsonSchema4 schema, object rootObject)
            : base(typeName, settings, resolver, schema, rootObject)
        {
            _typeName = typeName;
        }

        public new string Namespace
        {
            get
            {
                var split = _typeName.Split('.');
                return string.Join(".", split.Take(split.Length - 1));
            }
        }

        public new string ClassName
        {
            get
            {
                var split = _typeName.Split('.');
                return split.Last();
            }
        }
    }
}