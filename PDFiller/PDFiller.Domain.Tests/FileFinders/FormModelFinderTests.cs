using System;
using System.IO;
using PDFiller.Domain.FileFinders;
using Xunit;

namespace PDFiller.Domain.Tests.FileFinders
{
    public class FormModelFinderTests
    {
        [Fact]
        public void CanReachCSharpFormModelFile()
        {
            var applicationFolder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            var formModelFinder = new FormModelFinder(applicationFolder.FullName);

            var result = formModelFinder.GetPath();
            var formModelFile = new FileInfo(result);

            Assert.Equal(@"FormModel.cs", formModelFile.Name);
        }
    }
}
