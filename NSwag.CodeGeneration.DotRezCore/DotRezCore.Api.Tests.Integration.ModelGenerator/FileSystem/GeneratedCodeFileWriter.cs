using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DotRezCore.Api.Tests.Integration.ModelGenerator.CodeGeneration;
using DotRezCore.Api.Tests.Integration.ModelGenerator.FileSystem.Interfaces;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.FileSystem
{
    public class GeneratedCodeFileWriter : IGeneratedCodeFileWriter
    {
        private readonly IFileSystem _fileSystem;

        public GeneratedCodeFileWriter(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public async Task WriteAsync(GeneratedCode generatedCode, DotRezCodeGeneratorSettings settings)
        {
            await WriteAsync(generatedCode.ClientCode, settings.ClientFilesDestinationRoot, settings.DesiredClientNamespacePrefix);
            await WriteAsync(generatedCode.ContractCode, settings.ContractFilesDestinationRoot, settings.DesiredContractNamespacePrefix);
        }

        private async Task WriteAsync(string generatedCode, string destinationRoot, string namespacePrefix)
        {
            var syntaxRoot = await CSharpSyntaxTree.ParseText(generatedCode).GetRootAsync();

            foreach (var namespaceDeclarationSyntax in syntaxRoot.DescendantNodes().OfType<NamespaceDeclarationSyntax>())
            {
                foreach (var typeDeclarationSyntax in namespaceDeclarationSyntax.DescendantNodes().OfType<BaseTypeDeclarationSyntax>())
                {
                    var path = namespaceDeclarationSyntax.Name.ToString().Replace(namespacePrefix, string.Empty).Replace(".", string.Empty);
                    var filename = Path.Combine(destinationRoot, path, $"{typeDeclarationSyntax.Identifier.Text}.cs");

                    _fileSystem.EnsureDirectoryStructure(filename);
                    _fileSystem.WriteAllText(filename, namespaceDeclarationSyntax.ToString());   
                }
            }
        }
    }
}