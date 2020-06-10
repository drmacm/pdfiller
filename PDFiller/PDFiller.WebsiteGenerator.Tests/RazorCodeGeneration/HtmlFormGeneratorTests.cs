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
            var htmlFormGenerator = new HtmlFormGenerator();
            Action action = () => htmlFormGenerator.GenerateForm(null);

            var exception = Assert.Throws<ArgumentException>(action);

            Assert.Equal("List of form fields can't be null.", exception.Message);
        }

        [Fact]
        public void GenerateForm_FieldListEmpty_ShouldReturnEmptyString()
        {
            var htmlFormGenerator = new HtmlFormGenerator();

            var formHtml = htmlFormGenerator.GenerateForm(new List<FormField>());

            Assert.Equal(string.Empty, formHtml);
        }

        [Fact]
        public void GenerateForm_SingleTextField_ShouldReturnHtmlInputElement()
        {
            var htmlFormGenerator = new HtmlFormGenerator();
            var formField = new FormField("foo", FormFieldType.TextBox);

            var formHtml = htmlFormGenerator.GenerateForm(new List<FormField> { formField });

            var expectedHtml = $@"
    <div class=""form-group"" >
        <label for=""foo"">foo</label>
        <InputText id=""foo"" class=""form-control"" @bind-Value=""formModel.foo"" placeholder=""foo"" aria-describedby=""fooHelp""  />
        <small id=""fooHelp"" class=""form-text text-muted"" >foo</small>
    </div>".TrimStart(Environment.NewLine.ToCharArray());
            
            Assert.Equal(expectedHtml, formHtml);
        }


        [Fact]
        public void GenerateForm_SingleCheckboxField_ShouldReturnHtmlInputElement()
        {
            var htmlFormGenerator = new HtmlFormGenerator();
            var formField = new FormField("foo", FormFieldType.CheckBox);

            var formHtml = htmlFormGenerator.GenerateForm(new List<FormField> { formField });

            var expectedHtml = @"
    <div class=""form-group"" >
        <label class=""form-check-label"" for=""foo"">foo</label>
        <InputCheckbox id=""foo"" class=""form-check-input"" @bind-Value=""formModel.foo"" type=""checkbox"" />
    </div>".TrimStart(Environment.NewLine.ToCharArray());

            Assert.Equal(expectedHtml, formHtml);
        }

        [Fact]
        public void GenerateForm_TextFieldAndCheckboxField_ShouldReturnTwoInputElements()
        {
            var htmlFormGenerator = new HtmlFormGenerator();
            
            var formFields = new List<FormField>
            {
                new FormField("foo", FormFieldType.TextBox),
                new FormField("bar", FormFieldType.CheckBox),
            };

            var formHtml = htmlFormGenerator.GenerateForm(formFields);

            var expectedHtml = @"
    <div class=""form-group"" >
        <label for=""foo"">foo</label>
        <InputText id=""foo"" class=""form-control"" @bind-Value=""formModel.foo"" placeholder=""foo"" aria-describedby=""fooHelp""  />
        <small id=""fooHelp"" class=""form-text text-muted"" >foo</small>
    </div>

    <div class=""form-group"" >
        <label class=""form-check-label"" for=""bar"">bar</label>
        <InputCheckbox id=""bar"" class=""form-check-input"" @bind-Value=""formModel.bar"" type=""checkbox"" />
    </div>".TrimStart(Environment.NewLine.ToCharArray());

            Assert.Equal(expectedHtml, formHtml);
        }
    }
}
