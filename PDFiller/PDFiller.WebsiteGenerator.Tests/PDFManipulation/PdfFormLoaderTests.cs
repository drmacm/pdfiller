using PDFiller.WebsiteGenerator.PDFManipulation;
using Xunit;

namespace PDFiller.WebsiteGenerator.Tests.PDFManipulation
{
    public class PdfFormLoaderTests
    {
        [Fact]
        public void CanLoadPRP1Form()
        {
            var fileName = @"PDFManipulation\SamplePDFs\PRP-1-bos.pdf";
            var formFields = PdfFormLoader.GetFormFields(fileName);
            
            Assert.Equal(38, formFields.Count);
        }
    }
}
