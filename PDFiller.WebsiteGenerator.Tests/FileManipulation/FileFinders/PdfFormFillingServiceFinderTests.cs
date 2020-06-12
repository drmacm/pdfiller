using System;
using System.IO;
using PDFiller.WebsiteGenerator.FileManipulation.FileFinders;
using Xunit;

namespace PDFiller.WebsiteGenerator.Tests.FileManipulation.FileFinders
{
    public class PdfFormFillingServiceFinderTests
    {
        [Fact]
        public void CanReachPdfFormFillingServiceFile()
        {
            var applicationFolder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            var pdfFormFillingServiceFinder = new PdfFormFillingServiceFinder(applicationFolder.FullName);

            var result = pdfFormFillingServiceFinder.GetPath();
            var file = new FileInfo(result);

            Assert.True(file.Exists);
            Assert.Equal("PdfFormFillingService.cs", file.Name);
        }
    }
}
