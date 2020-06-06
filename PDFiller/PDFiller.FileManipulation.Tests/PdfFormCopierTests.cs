using System;
using System.IO;
using Xunit;

namespace PDFiller.FileManipulation.Tests
{
    public class PdfFormCopierTests
    {
        private readonly PdfFormFinder _pdfFormFinder;
        private readonly PdfFormCopier _pdfFormCopier;
        public PdfFormCopierTests()
        {
            _pdfFormFinder = new PdfFormFinder(AppDomain.CurrentDomain.BaseDirectory);
            _pdfFormCopier = new PdfFormCopier(_pdfFormFinder);
        }
        
        [Fact]
        public void CopyPdfFormToBlazorProject_PdfFormSourceNotProvided_ShouldThrow()
        {
            Action action = () => _pdfFormCopier.CopyPdfFormToBlazorProject(string.Empty);

            var exception = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Path to source PDF form not provided.", exception.Message);
        }

        [Fact]
        public void CopyPdfFormToBlazorProject_InvalidPathToPdfFormSource_ShouldThrow()
        {
            Action action = () => _pdfFormCopier.CopyPdfFormToBlazorProject(@"C:\IShouldNotExist.pdf");

            var exception = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Invalid path to source PDF form.", exception.Message);
        }

        [Fact]
        public void CanCopyPdfFormFile()
        {
            var pdfFormDestination = _pdfFormFinder.GetPathToPdfForm();
            if (File.Exists(pdfFormDestination))
            {
                File.Delete(pdfFormDestination);
            }

            Assert.False(File.Exists(pdfFormDestination));


            var pdfFormSource = @"SamplePDFs\PRP-1-bos.pdf";
            _pdfFormCopier.CopyPdfFormToBlazorProject(pdfFormSource);

            Assert.True(File.Exists(pdfFormDestination));
        }
    }
}
