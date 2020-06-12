using System;
using System.Collections.Generic;
using System.IO;
using PDFiller.Models;
using PDFiller.WebsiteGenerator.CSharpCodeGeneration;
using Xunit;

namespace PDFiller.WebsiteGenerator.Tests.CSharpCodeGeneration
{
    public class PdfFormFillingServiceGeneratorTests
    {
        private readonly char[] _newLineChars;
        private readonly PdfFormFillingServiceGenerator _pdfFormFillingServiceGenerator;

        public PdfFormFillingServiceGeneratorTests()
        {
            _newLineChars = Environment.NewLine.ToCharArray();
            _pdfFormFillingServiceGenerator = new PdfFormFillingServiceGenerator();
        }

        [Fact]
        public void Generate_PathToFormModelEmpty_ShouldThrow()
        {
            Action action = () => _pdfFormFillingServiceGenerator.Generate(new List<FormField>(), string.Empty);

            var exception = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Path to Pdf form filling service class not provided.", exception.Message);
        }

        [Fact]
        public void Generate_InvalidPathToFormModel_ShouldThrow()
        {
            Action action = () => _pdfFormFillingServiceGenerator.Generate(new List<FormField>(), @"C:\IShouldNotExist.cs");

            var exception = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Invalid path to Pdf form filling service class.", exception.Message);
        }

        [Fact]
        public void CanGeneratePdfFormFillingService()
        {
            var expectedSnippet = @"
            FillFormField(""First"", model.First);
            FillFormField(""Second"", model.Second);
".TrimStart(_newLineChars);

            var pathToPdfFormFillingService = @"CSharpCodeGeneration\SampleCodeFiles\SamplePdfFormFillingService.cs";

            var originalFormModelContent = File.ReadAllText(pathToPdfFormFillingService);
            Assert.DoesNotContain(expectedSnippet, originalFormModelContent);

            var formFields = new List<FormField>
            {
                new FormField("First", FormFieldType.TextBox),
                new FormField("Second", FormFieldType.TextBox),
            };

            var newFormModelContent = _pdfFormFillingServiceGenerator.Generate(formFields, pathToPdfFormFillingService);

            Assert.Contains(expectedSnippet, newFormModelContent);
        }
    }
}