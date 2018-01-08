using NJsonSchema;

namespace NSwag.CodeGeneration.DotRezCore
{
    public class DotRezTypeNameGenerator : DefaultTypeNameGenerator
    {
        /// <summary>Generates the type name for the given schema.</summary>
        /// <param name="schema">The schema.</param>
        /// <param name="typeNameHint">The type name hint.</param>
        /// <returns>The type name.</returns>
        protected override string Generate(JsonSchema4 schema, string typeNameHint)
        {
            var input = typeNameHint ?? "System.Object";
            input = input.Replace("DotRezCore.Api.Models", "DotRez.Api.Tests.Integration.Models");
            return input;
        }
    }
}