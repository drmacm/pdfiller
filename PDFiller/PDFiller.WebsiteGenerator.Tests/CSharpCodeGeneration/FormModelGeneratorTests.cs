using System;
using System.Collections.Generic;
using System.IO;
using PDFiller.Domain;
using PDFiller.WebsiteGenerator.CSharpCodeGeneration;
using Xunit;

namespace PDFiller.WebsiteGenerator.Tests.CSharpCodeGeneration
{
    public class FormModelGeneratorTests
    {
        private readonly FormModelGenerator _formModelGenerator;

        public FormModelGeneratorTests()
        {
            _formModelGenerator = new FormModelGenerator();
        }

        [Fact]
        public void Generate_PathToFormModelEmpty_ShouldThrow()
        {
            Action action = () => _formModelGenerator.Generate(new List<FormField>(), string.Empty);

            var exception = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Path to FormModel class not provided.", exception.Message);
        }

        [Fact]
        public void Generate_InvalidPathToFormModel_ShouldThrow()
        {
            Action action = () => _formModelGenerator.Generate(new List<FormField>(), @"C:\IShouldNotExist.cs");

            var exception = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Invalid path to FormModel class.", exception.Message);
        }

        [Fact]
        public void CanGenerateFormModel()
        {
            var pathToFormModel = @"CSharpCodeGeneration\SampleCodeFiles\SampleFormModel.cs";
            var expectedSnippet = "public string Baz { get; set; }";

            var originalFormModelContent = File.ReadAllText(pathToFormModel);
            Assert.DoesNotContain(expectedSnippet, originalFormModelContent);

            var formFields = new List<FormField>
            {
                new FormField("Baz", FormFieldType.TextBox)
            };
            
            var newFormModelContent = _formModelGenerator.Generate(formFields, pathToFormModel);

            Assert.Contains(expectedSnippet, newFormModelContent);
        }
    }
}