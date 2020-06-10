using System;
using System.IO;
using PDFiller.WebsiteGenerator.FileManipulation.FileFinders;
using Xunit;

namespace PDFiller.WebsiteGenerator.Tests.FileManipulation.FileFinders
{
    public class SampleFileFinder : FileFinder
    {
        private static readonly string SamplePath = "PDFiller.sln";

        public SampleFileFinder(string applicationFolder) : base(applicationFolder, SamplePath)
        {
        }
    }

    public class FileFinderWithoutRelativePath : FileFinder
    {
        private static readonly string RelativePathToHtmlForm = "";

        public FileFinderWithoutRelativePath(string applicationFolder) : base(applicationFolder, RelativePathToHtmlForm)
        {
        }
    }

    public class FileFinderTests
    {
        [Fact]
        public void ApplicationFolderEmptyString_ShouldThrow()
        {
            Action action = () => new SampleFileFinder(string.Empty);

            var exception = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Expecting a path to the folder of the executable.", exception.Message);
        }

        [Fact]
        public void RelativePathEmptyString_ShouldThrow()
        {
            var applicationFolder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)?.Parent;
            Action action = () => new FileFinderWithoutRelativePath(applicationFolder.FullName);

            var exception = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Expecting a relative path to the desired file.", exception.Message);
        }

        [Fact]
        public void ApplicationFolderIsNotNestedAsExpected_ShouldThrow()
        {
            var applicationFolder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)?.Parent;
            var fileFinder = new SampleFileFinder(applicationFolder.FullName);
            Action action = () => fileFinder.GetPath();

            var exception = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Unexpected folder structure.", exception.Message);
        }

        [Fact]
        public void CanReachSampleFile()
        {
            var applicationFolder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            var fileFinder = new SampleFileFinder(applicationFolder.FullName);

            var result = fileFinder.GetPath();
            var file = new FileInfo(result);

            Assert.True(file.Exists);
            Assert.Equal(@"PDFiller.sln", file.Name);
        }
    }
}
