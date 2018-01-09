using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NSwag.CodeGeneration.DotRezCore
{
    public class FileGenerator
    {
        public async Task Write(DotRezCodeGeneratorSettings settings)
        {
//            var syntaxRoot = await CSharpSyntaxTree.ParseText(mergedResults).GetRootAsync();
//
//            foreach (var namespaceDeclarationSyntax in syntaxRoot.DescendantNodes().OfType<NamespaceDeclarationSyntax>()
//            )
//            {
//                var typeDeclarationSyntax = namespaceDeclarationSyntax.DescendantNodes()
//                    .OfType<BaseTypeDeclarationSyntax>().SingleOrDefault();
//
//                var x = "DotRez.Api.Tests.Integration.Models.";
//                var destination = @"E:\NSwag.CodeGeneration.DotRezCore\DotRezCore.Api.Tests.Integration\Models";
//                var replace = namespaceDeclarationSyntax.Name.ToString()
//                    .Replace("DotRez.Api.Tests.Integration.Models", string.Empty).Replace(".", "\\");
//                var filename = Path.Combine(destination, replace, $"{typeDeclarationSyntax.Identifier.Text}.cs");
//                CreateFolderIfNeeded(filename);
//                File.WriteAllText(filename, namespaceDeclarationSyntax.ToString(), Encoding.UTF8);
//            }
        }

        /// <summary>
        /// Create the folder if not existing for a full file name
        /// </summary>
        /// <param name="filename">full path of the file</param>
        public static void EnsureDirectoryStructure(string filename)
        {
            string folder = System.IO.Path.GetDirectoryName(filename);
            if (!System.IO.Directory.Exists(folder))
            {
                System.IO.Directory.CreateDirectory(folder);
            }
        }
    }
}