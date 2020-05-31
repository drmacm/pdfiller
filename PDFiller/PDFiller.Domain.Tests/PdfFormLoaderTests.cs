using iText.Forms.Fields;
using PDFiller.Domain.Tests.Utilities;
using System.IO;
using System.Threading;
using Xunit;

namespace PDFiller.Domain.Tests
{
    public class PdfFormLoaderTests
    {
        [Fact]
        public void CanLoadPRP1Form()
        {
            var fileName = @"SamplePDFs\PRP-1-bos.pdf";
            var formFields = PdfFormLoader.GetFormFields(fileName);

            Assert.Equal(38, formFields.Count);
        }
    }
}
