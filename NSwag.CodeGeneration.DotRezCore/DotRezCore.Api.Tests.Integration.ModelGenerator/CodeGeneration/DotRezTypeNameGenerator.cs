using System.Linq;
using NJsonSchema;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.CodeGeneration
{
    internal class DotRezTypeNameGenerator : DefaultTypeNameGenerator
    {
        private readonly DotRezCodeGeneratorSettings _settings;

        public DotRezTypeNameGenerator(DotRezCodeGeneratorSettings settings)
        {
            _settings = settings;
        }

        /// <summary>Generates the type name for the given schema.</summary>
        /// <param name="schema">The schema.</param>
        /// <param name="typeNameHint">The type name hint.</param>
        /// <returns>The type name.</returns>
        protected override string Generate(JsonSchema4 schema, string typeNameHint)
        {
            if (string.IsNullOrEmpty(typeNameHint))
            {
                return "System.Object";
            }

            return typeNameHint.Replace(_settings.CurrentContractNamespacePrefix, _settings.DesiredContractNamespacePrefix);
        }
    }
}