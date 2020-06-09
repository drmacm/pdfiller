using System;
using System.IO;
using PDFiller.Domain.FileFinders;
using Xunit;

namespace PDFiller.Domain.Tests.FileFinders
{
    public class PdfFormFinderTests
    {
        [Fact]
        public void CanReachPdfFormFile()
        {
            var applicationFolder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            var pdfFormFinder = new PdfFormFinder(applicationFolder.FullName);

            var result = pdfFormFinder.GetPath();
            var htmlFormFile = new FileInfo(result);

            Assert.Equal("src.pdf", htmlFormFile.Name);
        }
    }
}
