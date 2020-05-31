using System;
using Xunit;

namespace PDFiller.Domain.Tests
{
    public class PdfPlaygroundTests
    {
        private readonly PdfPlayground pdfPlayground = new PdfPlayground();

        [Fact]
        public void FillUpTestPdf()
        {
            var source = @"SamplePDFs\PRP-1-bos.pdf";
            var destination = @"SamplePDFs\PRP-1-bos-filled.pdf";
            
            pdfPlayground.FillUpFormAsFile(source, destination);
        }
    }
}
