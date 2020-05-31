using iText.Forms.Fields;
using PDFiller.Domain.Tests.Utilities;
using System.IO;
using Xunit;

namespace PDFiller.Domain.Tests
{
    public class PdfFormGeneratorTests
    {
        [Fact]
        public void CanGenerateFormWithASingleTextField()
        {
            var fileName = @"SamplePDFs\TextBoxForm.pdf";
            var fields = new[]
            {
                new FormField("name", FormFieldType.TextBox)
            };
            PdfFormGenerator.GenerateForm(fileName, fields);

            var formFields = PdfFormLoader.GetFormFields(fileName);

            Assert.Single(formFields);
            Assert.Equal(FormFieldType.TextBox, formFields[0].FieldType);

            File.Delete(fileName);
        }

        [Fact]
        public void CanGenerateFormWithATextFieldAndCheckBox()
        {
            var fileName = @"SamplePDFs\TextBoxAndCheckBoxForm.pdf";

            var fields = new[]
            {
                new FormField("name", FormFieldType.TextBox),
                new FormField("enabled", FormFieldType.CheckBox)
            };
            PdfFormGenerator.GenerateForm(fileName, fields);

            var formFields = PdfFormLoader.GetFormFields(fileName);

            Assert.Equal(2, formFields.Count);
            Assert.Equal(FormFieldType.TextBox, formFields[0].FieldType);
            Assert.Equal(FormFieldType.CheckBox, formFields[1].FieldType);

            File.Delete(fileName);
        }
    }
}
