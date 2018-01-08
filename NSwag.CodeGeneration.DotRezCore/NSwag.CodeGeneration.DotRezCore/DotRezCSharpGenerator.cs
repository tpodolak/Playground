using System;
using System.Linq;
using NJsonSchema;
using NJsonSchema.CodeGeneration;
using NJsonSchema.CodeGeneration.CSharp;

namespace NSwag.CodeGeneration.DotRezCore
{
    public class DotRezCSharpGenerator : CSharpGenerator
    {
        private readonly CSharpTypeResolver _resolver;

        public DotRezCSharpGenerator(object rootObject, CSharpGeneratorSettings settings,
            CSharpTypeResolver resolver) : base(rootObject, settings, resolver)
        {
            _resolver = resolver;
        }

        public new string GenerateFile()
        {
            var result = string.Join(Environment.NewLine, GenerateTypes().Artifacts.Select(artifact => artifact.Code));

            return result;
        }

        protected override CodeArtifact GenerateType(JsonSchema4 schema, string typeNameHint)
        {
            var generateTypeName = _resolver.GetOrGenerateTypeName(schema, typeNameHint);
            return schema.IsEnumeration
                ? GenerateEnum(schema, generateTypeName)
                : GenerateClass(schema, generateTypeName);
        }

        private CodeArtifact GenerateClass(JsonSchema4 schema, string typeName)
        {
            var classTemplateModel =
                new DotRezClassTemplateModel(typeName, Settings, _resolver, schema, RootObject);
            var template =
                Settings.TemplateFactory.CreateTemplate("CSharp", CodeArtifactType.Class.ToString(),
                    classTemplateModel);
            return new CodeArtifact(typeName, classTemplateModel.BaseClassName, CodeArtifactType.Class,
                CodeArtifactLanguage.CSharp, template);
        }

        private CodeArtifact GenerateEnum(JsonSchema4 schema, string typeName)
        {
            var template = Settings.TemplateFactory.CreateTemplate("CSharp", CodeArtifactType.Enum.ToString(),
                new DotRezEnumTemplateModel(typeName, schema, Settings));

            return new CodeArtifact(typeName, CodeArtifactType.Enum, CodeArtifactLanguage.CSharp, template);
        }
    }
}