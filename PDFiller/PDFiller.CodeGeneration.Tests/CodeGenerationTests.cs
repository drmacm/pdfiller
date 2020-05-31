using Microsoft.CodeAnalysis.CSharp;
using System;
using System.IO;
using Xunit;

namespace PDFiller.CodeGeneration.Tests
{
    public class CodeGenerationTests
    {
        [Fact]
        public async void Test1()
        {
            var fileName = @"SampleCodeFiles\SamplePoco.cs";

            var code = await File.ReadAllTextAsync(fileName);

            var tree = CSharpSyntaxTree.ParseText(code);
        }
    }
}
