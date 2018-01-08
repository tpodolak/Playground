using System.Collections.Generic;

namespace NSwag.CodeGeneration.DotRezCore
{
    public class DotRezParameterNameGenerator : IParameterNameGenerator
    {
        public string Generate(SwaggerParameter parameter, IEnumerable<SwaggerParameter> allParameters)
        {
            return parameter.Name;
        }
    }
}