using System.Collections.Generic;
using NSwag;
using NSwag.CodeGeneration.CSharp.Models;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.CodeGeneration
{
    public class MethodOverloadDefinition
    {
        public MethodOverloadDefinition(CSharpOperationModel operation, List<CSharpParameterModel> parameters, List<CSharpParameterDescriptor> descriptors)
        {
            Operation = operation;
            Parameters = parameters;
            Descriptors = descriptors;
        }

        public CSharpOperationModel Operation { get; }

        public List<CSharpParameterModel> Parameters { get; }

        public List<CSharpParameterDescriptor> Descriptors { get; }
    }
}