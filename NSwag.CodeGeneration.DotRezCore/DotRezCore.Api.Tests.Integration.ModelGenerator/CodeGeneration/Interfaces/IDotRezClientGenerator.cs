using System.Threading.Tasks;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.CodeGeneration.Interfaces
{
    public interface IDotRezClientGenerator
    {
        Task<GeneratedCode> Generate(string swaggerSpecification, DotRezCodeGeneratorSettings settings);
    }
}