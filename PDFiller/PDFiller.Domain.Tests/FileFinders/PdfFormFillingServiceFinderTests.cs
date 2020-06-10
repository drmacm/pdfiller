using System;
using System.IO;
using PDFiller.Domain.FileFinders;
using Xunit;

namespace PDFiller.Domain.Tests.FileFinders
{
    public class PdfFormFillingServiceFinderTests
    {
        [Fact]
        public void CanReachPdfFormFillingServiceFile()
        {
            var applicationFolder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            var pdfFormFillingServiceFinder = new PdfFormFillingServiceFinder(applicationFolder.FullName);

            var result = pdfFormFillingServiceFinder.GetPath();
            var pdfFormFillingServiceFile = new FileInfo(result);

            Assert.Equal("PdfFormFillingService.cs", pdfFormFillingServiceFile.Name);
        }
    }
}
