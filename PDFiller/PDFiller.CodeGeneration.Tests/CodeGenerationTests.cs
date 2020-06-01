using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace PDFiller.CodeGeneration.Tests
{
    public class CodeGenerationTests
    {
        [Fact]
        public async void CanFindFooProperty()
        {
            var fileName = @"SampleCodeFiles\SamplePoco.cs";

            var code = await File.ReadAllTextAsync(fileName);

            var tree = CSharpSyntaxTree.ParseText(code);

            var syntaxRoot = tree.GetRoot();

            var fooProperty = syntaxRoot
                .DescendantNodes()
                .OfType<PropertyDeclarationSyntax>()
                .Where(p => p.Identifier.ValueText == "Foo")
                .FirstOrDefault();

            var barProperty = syntaxRoot
                .DescendantNodes()
                .OfType<PropertyDeclarationSyntax>()
                .Where(p => p.Identifier.ValueText == "Bar")
                .FirstOrDefault();
            Assert.NotNull(fooProperty);
            Assert.Null(barProperty);
        }
    }
}
