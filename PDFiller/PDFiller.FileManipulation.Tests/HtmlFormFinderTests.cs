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
            Action action = () => new HtmlFormFinder(string.Empty);

            var exception = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Expecting a path to the folder of the executable.", exception.Message);
        }

        [Fact]
        public void ApplicationFolderIsNotNestedAsExpected_ShouldThrow()
        {
            
            var applicationFolder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)?.Parent;
            var htmlFormFinder = new HtmlFormFinder(applicationFolder.FullName);
            Action action = () => htmlFormFinder.GetPathToHtmlForm();

            var exception = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Unexpected folder structure.", exception.Message);
        }

        [Fact]
        public void CanReachHtmlFormFile()
        {
            var htmlFormFinder = new HtmlFormFinder(AppDomain.CurrentDomain.BaseDirectory);
            
            var result = htmlFormFinder.GetPathToHtmlForm();
            var htmlFormFile = new FileInfo(result);
            
            Assert.Equal("HtmlForm.razor", htmlFormFile.Name);
        }
    }
}
