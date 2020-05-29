using System;
using Xunit;

namespace PDFiller.Core.Tests
{
    public class PdfPlaygroundTests
    {
        private readonly PdfPlayground pdfPlayground = new PdfPlayground();

        [Fact]
        public void Test1()
        {
            pdfPlayground.Play();
        }
    }
}
