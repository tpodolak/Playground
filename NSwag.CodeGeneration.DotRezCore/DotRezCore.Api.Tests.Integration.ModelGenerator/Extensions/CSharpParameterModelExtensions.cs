using NSwag;
using NSwag.CodeGeneration.CSharp.Models;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.Extensions
{
    public static class CSharpParameterModelExtensions
    {
        public static bool IsComplexQueryParameter( this CSharpParameterModel parameterModel)
        {
            return !(parameterModel.Schema is SwaggerParameter);
        }
    }
}