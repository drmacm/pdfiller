using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Xunit;

namespace PDFiller.CSharpCodeGeneration.Tests
{
    public class FormModelWriterTests
    {
        private readonly SyntaxTree _syntaxTree;
        private readonly FormModelWriter _formModelWriter;

        public FormModelWriterTests()
        {
            var fileName = @"SampleCodeFiles\SamplePoco.cs";
            var sourceCode = File.ReadAllText(fileName);

            _syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);

            _formModelWriter = new FormModelWriter(null);
        }

        [Fact]
        public void Test()
        {
            var result = _formModelWriter.Visit(_syntaxTree.GetRoot());
        }
    }
}
