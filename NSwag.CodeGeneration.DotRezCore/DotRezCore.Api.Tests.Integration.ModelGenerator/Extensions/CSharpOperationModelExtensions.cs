using System.Linq;
using NSwag.CodeGeneration.CSharp.Models;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.Extensions
{
    public static class CSharpOperationModelExtensions
    {
        public static bool RequiresXSessionToken(this CSharpOperationModel operationModel)
        {
            return operationModel.Parameters.Any(parameter => parameter.IsXSessionTokenParameter());
        }

        public static CSharpParameterModel GetXSessionTokenParameter(this CSharpOperationModel operationModel)
        {
            return operationModel.Parameters.Single(parameter => parameter.IsXSessionTokenParameter());
        }
    }
}