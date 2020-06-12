using System;
using System.IO;
using PDFiller.WebsiteGenerator.FileManipulation.FileFinders;
using Xunit;

namespace PDFiller.WebsiteGenerator.Tests.FileManipulation.FileFinders
{
    public class FormModelFinderTests
    {
        [Fact]
        public void CanReachCSharpFormModelFile()
        {
            var applicationFolder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            var formModelFinder = new FormModelFinder(applicationFolder.FullName);

            var result = formModelFinder.GetPath();
            var file = new FileInfo(result);
            
            Assert.True(file.Exists);
            Assert.Equal(@"FormModel.cs", file.Name);
        }
    }
}
