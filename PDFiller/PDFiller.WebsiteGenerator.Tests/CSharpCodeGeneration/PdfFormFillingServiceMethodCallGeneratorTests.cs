using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using PDFiller.Models;
using PDFiller.WebsiteGenerator.CSharpCodeGeneration;
using Xunit;

namespace PDFiller.WebsiteGenerator.Tests.CSharpCodeGeneration
{
    public class PdfFormFillingServiceMethodCallGeneratorTests
    {
        private readonly char[] _newLineChars;
        private readonly SyntaxTree _syntaxTree;

        public PdfFormFillingServiceMethodCallGeneratorTests()
        {
            _newLineChars = Environment.NewLine.ToCharArray();
            var fileName = @"CSharpCodeGeneration\SampleCodeFiles\SamplePdfFormFillingService.cs";
            var sourceCode = File.ReadAllText(fileName);

            _syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
        }

        [Fact]
        public void FormFieldNull_ReturnsUnchangedModel()
        {
            var expectedSnippet = @"FillFormField("""; //a call FillFormField method

            var pdfFormFillingServiceMethodCallGenerator = new PdfFormFillingServiceMethodCallGenerator(null);
            var result = pdfFormFillingServiceMethodCallGenerator.Visit(_syntaxTree.GetRoot()).ToFullString();

            Assert.DoesNotContain(expectedSnippet, result);
        }

        [Fact]
        public void FormFieldListEmpty_ReturnsUnchangedModel()
        {
            var expectedSnippet = @"FillFormField("""; //a call FillFormField method

            var pdfFormFillingServiceMethodCallGenerator = new PdfFormFillingServiceMethodCallGenerator(null);
            var result = pdfFormFillingServiceMethodCallGenerator.Visit(_syntaxTree.GetRoot()).ToFullString();

            Assert.DoesNotContain(expectedSnippet, result);
        }

        [Fact]
        public void MultipleFormFields_CanGenerateFormModel()
        {
            var expectedSnippet = @"
            FillFormField(""First"", model.First);
            FillFormField(""Second"", model.Second);
".TrimStart(_newLineChars); 

            var formFields = new List<FormField>
            {
                new FormField("First", FormFieldType.TextBox),
                new FormField("Second", FormFieldType.TextBox),
            };

            var pdfFormFillingServiceMethodCallGenerator = new PdfFormFillingServiceMethodCallGenerator(formFields);
            var result = pdfFormFillingServiceMethodCallGenerator.Visit(_syntaxTree.GetRoot()).ToFullString();

            Assert.Contains(expectedSnippet, result);
        }
    }
}