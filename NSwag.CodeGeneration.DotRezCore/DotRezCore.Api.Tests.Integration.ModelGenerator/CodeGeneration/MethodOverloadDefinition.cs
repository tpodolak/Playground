using System.Collections.Generic;
using NSwag;
using NSwag.CodeGeneration.CSharp.Models;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.CodeGeneration
{
    public class MethodOverloadDefinition
    {
        public CSharpOperationModel Operation { get; set; }
        
        public List<CSharpParameterModel> Parameters { get; set; }

        public List<CSharpParameterDescriptor> Descriptors { get; set; }
    }

    public class CSharpParameterDescriptor
    {
        public string VariableName { get; set; }

        public string Substitute { get; set; }
    }
}