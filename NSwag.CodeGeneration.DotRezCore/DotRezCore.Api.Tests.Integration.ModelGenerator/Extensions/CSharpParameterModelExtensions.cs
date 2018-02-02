using NSwag;
using NSwag.CodeGeneration.CSharp.Models;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.Extensions
{
    public static class CSharpParameterModelExtensions
    {
        public static bool IsComplexQueryParameter(this CSharpParameterModel parameterModel)
        {
            return !(parameterModel.Schema is SwaggerParameter);
        }

        public static bool IsXSessionTokenParameter(this CSharpParameterModel parameterModel)
        {
            return parameterModel.Kind == SwaggerParameterKind.Header && parameterModel.Name == Constants.Session.XSessionTokenHeaderName;
        }

        public static bool IsCultureParameter(this CSharpParameterModel parameterModel)
        {
            return parameterModel.Kind == SwaggerParameterKind.Path && parameterModel.Name == Constants.UrlPath.CulturePath;
        }
    }
}