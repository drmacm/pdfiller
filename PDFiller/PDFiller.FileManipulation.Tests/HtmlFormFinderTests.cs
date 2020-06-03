using System;
using System.IO;
using Xunit;

namespace PDFiller.FileManipulation.Tests
{
    public class HtmlFormFinderTests
    {
        [Fact]
        public void ApplicationFolderEmptyString_ShouldThrow()
        {
            Action func = () => new HtmlFormFinder(string.Empty);

            var exception = Assert.Throws<ArgumentException>(func);

            Assert.Equal("Expecting a path to the folder of the executable.", exception.Message);
        }

        [Fact]
        public void ApplicationFolderIsNotNestedAsExpected_ShouldThrow()
        {
            
            var applicationFolder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)?.Parent;
            Action func = () => new HtmlFormFinder(applicationFolder.FullName);

            var exception = Assert.Throws<ArgumentException>(func);

            Assert.Equal("Unexpected folder structure.", exception.Message);
        }

        [Fact]
        public void CanReachHtmlFormFile()
        {
            var htmlFormFinder = new HtmlFormFinder(AppDomain.CurrentDomain.BaseDirectory);

            var pathToHtmlForm = htmlFormFinder.PathToHtmlForm;

            Assert.True(File.Exists(pathToHtmlForm));
        }
    }
}
