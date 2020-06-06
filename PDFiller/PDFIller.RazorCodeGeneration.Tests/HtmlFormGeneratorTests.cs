using System;
using System.Collections.Generic;
using PDFiller.Domain;
using Xunit;

namespace PDFiller.RazorCodeGeneration.Tests
{
    public class HtmlFormGeneratorTests
    {
        [Fact]
        public void GenerateForm_FieldListNull_ShouldThrow()
        {
            var htmlFormGenerator = new HtmlFormGenerator(new FragmentRenderer());
            Action action = () => htmlFormGenerator.GenerateForm(null);

            var exception = Assert.Throws<ArgumentException>(action);

            Assert.Equal("List of form fields can't be null.", exception.Message);
        }

        [Fact]
        public void GenerateForm_FieldListEmpty_ShouldReturnEmptyString()
        {
            var htmlFormGenerator = new HtmlFormGenerator(new FragmentRenderer());

            var formHtml = htmlFormGenerator.GenerateForm(new List<FormField>());

            Assert.Equal(string.Empty, formHtml);
        }

        [Fact]
        public void GenerateForm_SingleTextField_ShouldReturnHtmlInputElement()
        {
            var htmlFormGenerator = new HtmlFormGenerator(new FragmentRenderer());
            var formField = new FormField("foo", FormFieldType.TextBox);

            var formHtml = htmlFormGenerator.GenerateForm(new List<FormField> { formField });

            Assert.Equal($@"<label for=""foo"">foo</label>{Environment.NewLine}<input id=""foo"" name=""foo"" required=""required"" />", formHtml);
        }
    }
}
