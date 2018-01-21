using System.Collections.Generic;
using System.Linq;
using NSwag;
using NSwag.CodeGeneration;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.CodeGeneration
{
    internal class DotRezParameterNameGenerator : IParameterNameGenerator
    {
        public string Generate(SwaggerParameter parameter, IEnumerable<SwaggerParameter> allParameters)
        {
            var parameterName = parameter.Name.Replace("-", string.Empty).Replace(".", string.Empty).Replace("$", string.Empty).ToLower();
            if (allParameters.Count(innerParameter => innerParameter.Name == parameter.Name) > 1)
                return parameterName + parameter.Kind;
            return parameterName;
        }
    }
}