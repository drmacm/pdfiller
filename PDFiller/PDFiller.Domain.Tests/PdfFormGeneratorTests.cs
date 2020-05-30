using iText.Forms.Fields;
using System.IO;
using Xunit;

namespace PDFiller.Domain.Tests
{
    public class PdfFormGeneratorTests
    {
        [Fact]
        public void CanGenerateFormWithASingleTextField()
        {
            var fileName = "PDFiller-SingleTextBoxForm.pdf";
            var _formGenerator = new PdfFormGenerator(fileName);
            _formGenerator.GenerateFormWithSingleTextField();

            var _formLoader = new PdfFormLoader(fileName);

            var formFields = _formLoader.GetFormFields();

            Assert.Equal(1, formFields.Count);
            Assert.Equal(typeof(PdfTextFormField), formFields["name"].GetType());

            File.Delete(fileName);
        }
    }
}
