using System;
using System.IO;
using PDFiller.Domain.FileFinders;
using Xunit;

namespace PDFiller.Domain.Tests.FileFinders
{
    public class HtmlFormFinderTests
    {
        [Fact]
        public void CanReachHtmlFormFile()
        {
            var htmlFormFinder = new HtmlFormFinder(AppDomain.CurrentDomain.BaseDirectory);
            
            var result = htmlFormFinder.GetPath();
            var htmlFormFile = new FileInfo(result);
            
            Assert.Equal("HtmlForm.razor", htmlFormFile.Name);
        }
    }
}
