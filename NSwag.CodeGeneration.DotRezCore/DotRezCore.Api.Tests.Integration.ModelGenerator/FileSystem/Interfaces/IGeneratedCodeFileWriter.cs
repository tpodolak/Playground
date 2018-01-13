using System.Threading.Tasks;
using DotRezCore.Api.Tests.Integration.ModelGenerator.CodeGeneration;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.FileSystem.Interfaces
{
    public interface IGeneratedCodeFileWriter
    {
        Task WriteAsync(GeneratedCode generatedCode, DotRezCodeGeneratorSettings settings);
    }
}