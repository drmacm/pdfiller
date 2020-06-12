using PDFiller.WebsiteGenerator.PDFManipulation;
using Xunit;

namespace PDFiller.WebsiteGenerator.Tests.PDFManipulation
{
    public class PdfPlaygroundTests
    {
        private readonly PdfPlayground pdfPlayground = new PdfPlayground();

        [Fact]
        public void FillUpTestPdf()
        {
            var source = @"PDFManipulation\SamplePDFs\PRP-1-bos.pdf";
            var destination = @"PDFManipulation\SamplePDFs\PRP-1-bos-filled.pdf";
            
            pdfPlayground.FillUpFormAsFile(source, destination);
        }
    }
}
