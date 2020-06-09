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
            var csharpFormModelFinder = new FormModelFinder(applicationFolder.FullName);

            var result = csharpFormModelFinder.GetPath();
            var csharpFormModelFile = new FileInfo(result);

            Assert.EndsWith(@"FormModel.cs", result);
        }
    }
}
