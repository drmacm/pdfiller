using System;
using System.Collections.Generic;
using PDFiller.Domain;
using PDFiller.WebsiteGenerator.RazorCodeGeneration;
using Xunit;

namespace PDFiller.WebsiteGenerator.Tests.RazorCodeGeneration
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

            var expectedHtml =
                $@"<label for=""foo"">foo</label>
<input id=""foo"" name=""foo"" required=""required"" />";
            
            Assert.Equal(expectedHtml, formHtml);
        }


        [Fact]
        public void GenerateForm_SingleCheckboxField_ShouldReturnHtmlInputElement()
        {
            var htmlFormGenerator = new HtmlFormGenerator(new FragmentRenderer());
            var formField = new FormField("foo", FormFieldType.CheckBox);

            var formHtml = htmlFormGenerator.GenerateForm(new List<FormField> { formField });

            var expectedHtml =
                @"<label for=""foo"">foo</label>
<input id=""foo"" name=""foo"" type=""checkbox"" required=""required"" />";

            Assert.Equal(expectedHtml, formHtml);
        }

        [Fact]
        public void GenerateForm_TextFieldAndCheckboxField_ShouldReturnTwoInputElements()
        {
            var htmlFormGenerator = new HtmlFormGenerator(new FragmentRenderer());
            
            var formFields = new List<FormField>
            {
                new FormField("foo", FormFieldType.TextBox),
                new FormField("bar", FormFieldType.CheckBox),
            };

            var formHtml = htmlFormGenerator.GenerateForm(formFields);

            var expectedHtml =
                @"<label for=""foo"">foo</label>
<input id=""foo"" name=""foo"" required=""required"" />

<label for=""bar"">bar</label>
<input id=""bar"" name=""bar"" type=""checkbox"" required=""required"" />";

            Assert.Equal(expectedHtml, formHtml);
        }
    }
}
