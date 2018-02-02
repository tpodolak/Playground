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
            if (cultureParameter != null)
            {
                var methodOverloadDefinition = new MethodOverloadDefinition
                {
                    Operation = operationModel,
                    Parameters = operationModel.Parameters.Except(new[] {cultureParameter}).ToList(),
                    Descriptors = new List<CSharpParameterDescriptor>
                    {
                        new CSharpParameterDescriptor
                        {
                            VariableName = cultureParameter.VariableName,
                            Substitute = "DefaultCulture"
                        }
                    }
                };
                yield return methodOverloadDefinition;
            }

            if (sessionParameter != null)
            {
                yield return new MethodOverloadDefinition
                {
                    Operation = operationModel,
                    Parameters = operationModel.Parameters.Except(new[] {sessionParameter}).ToList(),
                    Descriptors = new List<CSharpParameterDescriptor>
                    {
                        new CSharpParameterDescriptor
                        {
                            VariableName = sessionParameter.VariableName,
                            Substitute = "XSessionToken"
                        }
                    }
                };
            }

            if (sessionParameter != null && cultureParameter != null)
            {
                yield return new MethodOverloadDefinition
                {
                    Operation = operationModel,
                    Parameters = operationModel.Parameters.Except(new[] {cultureParameter, sessionParameter}).ToList(),
                    Descriptors = new List<CSharpParameterDescriptor>
                    {
                        new CSharpParameterDescriptor
                        {
                            VariableName = cultureParameter.VariableName,
                            Substitute = "DefaultCulture"
                        },
                        new CSharpParameterDescriptor
                        {
                            VariableName = sessionParameter.VariableName,
                            Substitute = "XSessionToken"
                        }
                    }
                };
            }
        }
    }
}