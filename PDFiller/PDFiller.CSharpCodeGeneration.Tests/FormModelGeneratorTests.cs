using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using PDFiller.Domain;
using Xunit;

namespace PDFiller.CSharpCodeGeneration.Tests
{
    public class FormModelGeneratorTests
    {
        private readonly char[] _newLineChars;
        private readonly SyntaxTree _syntaxTree;

        public FormModelGeneratorTests()
        {
            _newLineChars = Environment.NewLine.ToCharArray();
            var fileName = @"SampleCodeFiles\SampleFormModel.cs";
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

            var formModelGenerator = new FormModelGenerator(null);
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

            var formModelGenerator = new FormModelGenerator(new List<FormField>());
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
        public string SomeStringPropertyC { get; set; }

        [Required]
        public bool SomeBoolPropertyC { get; set; }
    }
}".TrimStart(_newLineChars);

            var formFields = new List<FormField>
            {
                new FormField("some string property č", FormFieldType.TextBox),
                new FormField("some bool property č", FormFieldType.CheckBox),
            };

            var formModelGenerator = new FormModelGenerator(formFields);
            var result = formModelGenerator.Visit(_syntaxTree.GetRoot()).ToFullString();

            Assert.Equal(expectedResult, result);
        }
    }
}