using System;
using System.IO;
using PDFiller.Domain.FileFinders;
using Xunit;

namespace PDFiller.Domain.Tests.FileFinders
{
    public class FakeFileFinder : FileFinder
    {
        private static readonly string RelativePathToHtmlForm = @"SampleFiles\Sample.txt";

        public FakeFileFinder(string applicationFolder) : base(applicationFolder, RelativePathToHtmlForm)
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
            Action action = () => new FakeFileFinder(string.Empty);

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
            var htmlFormFinder = new FakeFileFinder(applicationFolder.FullName);
            Action action = () => htmlFormFinder.GetPath();

            var exception = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Unexpected folder structure.", exception.Message);
        }
    }
}
