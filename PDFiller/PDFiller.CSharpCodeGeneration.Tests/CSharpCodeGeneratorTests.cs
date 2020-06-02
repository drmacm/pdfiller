using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Xunit;

namespace PDFiller.CSharpCodeGeneration.Tests
{
    public class CSharpCodeGeneratorTests
    {
        private readonly SyntaxTree _syntaxTree;

        public CSharpCodeGeneratorTests()
        {
            var fileName = @"SampleCodeFiles\SamplePoco.cs";
            var sourceCode = File.ReadAllText(fileName);

            _syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
        }

        [Fact]
        public void CanFindFooProperty()
        {
            var fooProperty = _syntaxTree
                .GetRoot()
                .DescendantNodes()
                .OfType<PropertyDeclarationSyntax>()
                .FirstOrDefault(p => p.Identifier.ValueText == "Foo");

            Assert.NotNull(fooProperty);
        }

        [Fact]
        public void CanNotFindBarProperty()
        {
            var barProperty = _syntaxTree
                .GetRoot()
                .DescendantNodes()
                .OfType<PropertyDeclarationSyntax>()
                .FirstOrDefault(p => p.Identifier.ValueText == "Bar");

            Assert.Null(barProperty);
        }


        [Fact]
        public void CanFindBarMethod()
        {
            var barMethod = _syntaxTree
                .GetRoot()
                .DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .FirstOrDefault(p => p.Identifier.ValueText == "Bar");

            Assert.NotNull(barMethod);
        }
    }
}
