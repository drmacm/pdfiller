using System;
using System.IO;
using Xunit;

namespace PDFiller.FileManipulation.Tests
{
    public class PdfFormFinderTests
    {
        [Fact]
        public void ApplicationFolderEmptyString_ShouldThrow()
        {
            Action action = () => new PdfFormFinder(string.Empty);

            var exception = Assert.Throws<ArgumentException>(action);
            Assert.Equal("Expecting a path to the folder of the executable.", exception.Message);
        }

        [Fact]
        public void ApplicationFolderIsNotNestedAsExpected_ShouldThrow()
        {
            var applicationFolder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)?.Parent;
            var pdfFormFinder = new PdfFormFinder(applicationFolder.FullName);

            Action action = () => pdfFormFinder.GetPathToPdfForm();

            var exception = Assert.Throws<ArgumentException>(action);
            Assert.Equal("Unexpected folder structure.", exception.Message);
        }

        [Fact]
        public void ApplicationFolderIsAsExpected_ShouldReturnCorrectFolder()
        {
            var applicationFolder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            var pdfFormFinder = new PdfFormFinder(applicationFolder.FullName);

            var result = pdfFormFinder.GetPathToPdfForm();

            Assert.EndsWith(@"PDFiller\wwwroot\sample-data\src.pdf", result);
        }
    }
}
