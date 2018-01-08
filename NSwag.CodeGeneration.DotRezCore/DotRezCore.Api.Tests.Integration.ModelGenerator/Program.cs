using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NSwag.CodeGeneration.DotRezCore;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var schemaRetriever = new SchemaRetriever();
            var schema = schemaRetriever.RetrieveSchema("Sample API");

//            var schema =
//                File.ReadAllText(
//                    @"E:\NSwag.CodeGeneration.DotRezCore\NSwag.CodeGeneration.DotRezCore.Tests.Integration\Data\Input\GenerateClient\DotRezV3.json");
            
            var generator = new DotRezSwaggerClientGenerator();

            var models = await generator.Generate(schema);
        }
    }
}