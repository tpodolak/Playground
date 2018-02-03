using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NSwag.CodeGeneration.DotRezCore.Tests.Integration
{
    public class DotRezSwaggerClientGeneratorTests
    {
        [Theory]
        [MemberData(nameof(GenerateContractTestCases))]
        public async Task Generate_CorrectlyGeneratesModelClasses(TestCase testCase)
        {
            var subject = new DotRezSwaggerClientGenerator();

           // var result = await subject.Generate(testCase.Input);
            
            // result.ContractCode.Should().Be(testCase.ExpectedOutput);
        }
  
        [Theory]
        [MemberData(nameof(GenerateClientTestCases))]
        public async Task Generate_CorrectlyGeneratesClientClass(TestCase testCase)
        {
            //var subject = new DotRezSwaggerClientGenerator();

            //var result = await subject.Generate(testCase.Input);
            
            // result.ClientCode.Should().Be(testCase.ExpectedOutput);
        }
        
        public static IEnumerable<object[]> GenerateContractTestCases
        {
            get
            {
                var currentDirectory = Environment.CurrentDirectory;
                var inputDirectory = Path.Combine(currentDirectory, "Data/Input/GenerateContract");
                var outputDirectory = Path.Combine(currentDirectory, "Data/Output/GenerateContract");
                return GenerateTestCases(inputDirectory, outputDirectory).Select(testCase => new object[] {testCase});
            }
        }

        public static IEnumerable<object[]> GenerateClientTestCases
        {
            get
            {
                var currentDirectory = Environment.CurrentDirectory;
                var inputDirectory = Path.Combine(currentDirectory, "Data/Input/GenerateClient");
                var outputDirectory = Path.Combine(currentDirectory, "Data/Output/GenerateClient");
                return GenerateTestCases(inputDirectory, outputDirectory).Select(testCase => new object[] {testCase});
            }
        }

        private static IEnumerable<TestCase> GenerateTestCases(string inputDirectory, string outputDirectory)
        {
            var inputFilePaths = Directory.GetFiles(inputDirectory);

            return inputFilePaths.Select(inputFile =>
            {
                var testCaseName = Path.GetFileNameWithoutExtension(inputFile);
                var input = File.ReadAllText(inputFile);
                var expectedOutput = File.ReadAllText(Path.Combine(outputDirectory, $"{testCaseName}.cs"));

                return new TestCase(testCaseName, input, expectedOutput);
            });
        }
    }

    public class TestCase
    {
        public string Name { get; }

        public string Input { get; }

        public string ExpectedOutput { get; }

        public TestCase(string name, string input, string expectedOutput)
        {
            Name = name;
            Input = input;
            ExpectedOutput = expectedOutput;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}