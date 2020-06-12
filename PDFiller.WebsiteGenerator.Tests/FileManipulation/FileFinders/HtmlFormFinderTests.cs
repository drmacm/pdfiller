using System;
using System.IO;
using PDFiller.WebsiteGenerator.FileManipulation.FileFinders;
using Xunit;

namespace PDFiller.WebsiteGenerator.Tests.FileManipulation.FileFinders
{
    public class HtmlFormFinderTests
    {
        [Fact]
        public void CanReachHtmlFormFile()
        {
            var htmlFormFinder = new HtmlFormFinder(AppDomain.CurrentDomain.BaseDirectory);
            
            var result = htmlFormFinder.GetPath();
            var file = new FileInfo(result);

            Assert.True(file.Exists);
            Assert.Equal("HtmlForm.razor", file.Name);
        }
    }
}
