using System;
using System.IO;
using PDFiller.WebsiteGenerator.FileManipulation.FileFinders;
using Xunit;

namespace PDFiller.WebsiteGenerator.Tests.FileManipulation.FileFinders
{
    public class PdfFormFinderTests
    {
        [Fact]
        public void CanReachPdfFormFile()
        {
            var applicationFolder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            var pdfFormFinder = new PdfFormFinder(applicationFolder.FullName);

            var result = pdfFormFinder.GetPath();
            var file = new FileInfo(result);

            Assert.True(file.Exists);
            Assert.Equal("src.pdf", file.Name);
        }
    }
}
