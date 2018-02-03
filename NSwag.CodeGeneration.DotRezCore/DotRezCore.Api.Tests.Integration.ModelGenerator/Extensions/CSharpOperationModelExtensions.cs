using System.Collections.Generic;
using System.Linq;
using DotRezCore.Api.Tests.Integration.ModelGenerator.CodeGeneration;
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

        public static CSharpParameterModel GetCultureParameter(this CSharpOperationModel operationModel)
        {
            return operationModel.Parameters.Single(parameter => parameter.IsCultureParameter());
        }

        public static IEnumerable<MethodOverloadDefinition> GetOverloads(this CSharpOperationModel operationModel)
        {
            var cultureParameter = operationModel.Parameters.SingleOrDefault(parameter => parameter.IsCultureParameter());
            var sessionParameter = operationModel.Parameters.SingleOrDefault(parameter => parameter.IsXSessionTokenParameter());
            MethodOverloadDefinition cultureOverloadDefinition = null;
            MethodOverloadDefinition sessionOverloadDefinition = null;

            if (cultureParameter != null)
            {
                var descriptors = new List<CSharpParameterDescriptor>
                {
                    new CSharpParameterDescriptor(cultureParameter.VariableName, Constants.VariableNames.DefaultCulturePropertyName)
                };

                cultureOverloadDefinition = new MethodOverloadDefinition(operationModel,
                    operationModel.Parameters.Except(new[] {cultureParameter}).ToList(),
                    descriptors);

                yield return cultureOverloadDefinition;
            }

            if (sessionParameter != null)
            {
                var descriptors = new List<CSharpParameterDescriptor>
                {
                    new CSharpParameterDescriptor(sessionParameter.VariableName, Constants.VariableNames.XSessionTokenPropertyName)
                };

                sessionOverloadDefinition = new MethodOverloadDefinition(operationModel,
                    operationModel.Parameters.Except(new[] {sessionParameter}).ToList(),
                    descriptors);

                yield return sessionOverloadDefinition;
            }

            if (cultureOverloadDefinition != null && sessionOverloadDefinition != null)
            {
                var descriptors = cultureOverloadDefinition.Descriptors.Union(sessionOverloadDefinition.Descriptors).ToList();
                yield return new MethodOverloadDefinition(operationModel,
                    operationModel.Parameters.Except(new[] {cultureParameter, sessionParameter}).ToList(),
                    descriptors);

                yield return sessionOverloadDefinition;
            }
        }
    }
}