using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using PDFiller.Domain;
using PDFiller.WebsiteGenerator.CSharpCodeGeneration;
using Xunit;

namespace PDFiller.WebsiteGenerator.Tests.CSharpCodeGeneration
{
    public class FormFieldPropertyGeneratorTests
    {
        private readonly char[] _newLineChars;
        private readonly SyntaxTree _syntaxTree;

        public FormFieldPropertyGeneratorTests()
        {
            _newLineChars = Environment.NewLine.ToCharArray();
            var fileName = @"CSharpCodeGeneration\SampleCodeFiles\SampleFormModel.cs";
            var sourceCode = File.ReadAllText(fileName);

            _syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
        }

        [Fact]
        public void FormFieldNull_ReturnsUnchangedModel()
        {
            var expectedResult = @"
namespace PDFiller.CodeGeneration.Tests.SampleCodeFiles
{
    class SampleFormModel
    {
    }
}".TrimStart(_newLineChars);

            var formModelGenerator = new FormFieldPropertyGenerator(null);
            var result = formModelGenerator.Visit(_syntaxTree.GetRoot()).ToFullString();

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void FormFieldListEmpty_ReturnsUnchangedModel()
        {
            var expectedResult = @"
namespace PDFiller.CodeGeneration.Tests.SampleCodeFiles
{
    class SampleFormModel
    {
    }
}".TrimStart(_newLineChars);

            var formModelGenerator = new FormFieldPropertyGenerator(new List<FormField>());
            var result = formModelGenerator.Visit(_syntaxTree.GetRoot()).ToFullString();

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void MultipleFormFields_CanGenerateFormModel()
        {
            var expectedResult = @"
namespace PDFiller.CodeGeneration.Tests.SampleCodeFiles
{
    class SampleFormModel
    {
        [Required]
        public string First { get; set; }

        [Required]
        public bool Second { get; set; }
    }
}".TrimStart(_newLineChars);

            var formFields = new List<FormField>
            {
                new FormField("First", FormFieldType.TextBox),
                new FormField("Second", FormFieldType.CheckBox),
            };

            var formModelGenerator = new FormFieldPropertyGenerator(formFields);
            var result = formModelGenerator.Visit(_syntaxTree.GetRoot()).ToFullString();

            Assert.Equal(expectedResult, result);
        }
    }
}