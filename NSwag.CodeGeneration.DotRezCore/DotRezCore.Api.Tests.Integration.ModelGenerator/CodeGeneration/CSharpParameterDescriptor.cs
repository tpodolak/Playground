namespace DotRezCore.Api.Tests.Integration.ModelGenerator.CodeGeneration
{
    public class CSharpParameterDescriptor
    {
        public CSharpParameterDescriptor(string variableName, string substitute)
        {
            VariableName = variableName;
            Substitute = substitute;
        }

        public string VariableName { get; }

        public string Substitute { get; }
    }
}