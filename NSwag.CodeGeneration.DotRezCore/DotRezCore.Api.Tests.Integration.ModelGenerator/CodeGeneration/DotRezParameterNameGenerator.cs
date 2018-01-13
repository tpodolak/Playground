using System.Collections.Generic;
using NSwag;
using NSwag.CodeGeneration;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.CodeGeneration
{
    internal class DotRezParameterNameGenerator : IParameterNameGenerator
    {
        public string Generate(SwaggerParameter parameter, IEnumerable<SwaggerParameter> allParameters)
        {
            return parameter.Name;
        }
    }
}