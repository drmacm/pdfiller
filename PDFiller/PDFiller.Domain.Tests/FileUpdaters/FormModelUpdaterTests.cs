using System;
using System.Globalization;
using System.IO;
using PDFiller.Domain.FileFinders;
using PDFiller.Domain.FileUpdaters;
using Xunit;

namespace PDFiller.Domain.Tests.FileUpdaters
{
    public class FormModelUpdaterTests
    {
        [Fact]
        public void UpdateFormModel_PathToFormModelmNotProvided_ShouldThrow()
        {
            var formModelUpdater = new FormModelUpdater();
            Action action = () => formModelUpdater.UpdateFormModel(string.Empty, string.Empty);

            var exception = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Path to FormModel class not provided.", exception.Message);
        }

        [Fact]
        public void UpdateFormModel_InvalidPathToFormModel_ShouldThrow()
        {
            var formModelUpdater = new FormModelUpdater();
            Action action = () => formModelUpdater.UpdateFormModel(@"C:\IShouldNotExist.razor", string.Empty);

            var exception = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Invalid path to FormModel class.", exception.Message);
        }

        [Fact]
        public void UpdateFormModel_SourceCodeNotProvided_ShouldThrow()
        {
            var formModelFinder = new FormModelFinder(AppDomain.CurrentDomain.BaseDirectory);
            var pathToFormModel = formModelFinder.GetPath();
            var formModelUpdater = new FormModelUpdater();
            Action action = () => formModelUpdater.UpdateFormModel(pathToFormModel, string.Empty);

            var exception = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Source code is null or empty.", exception.Message);
        }

        [Fact]
        public void CanUpdateFormModel()
        {
            var formModelFinder = new FormModelFinder(AppDomain.CurrentDomain.BaseDirectory);
            var pathToFormModel = formModelFinder.GetPath();
            var formMarkup = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);

            var formModelUpdater = new FormModelUpdater();

            var originalContent = File.ReadAllText(pathToFormModel);
            Assert.DoesNotContain(formMarkup, originalContent);

            formModelUpdater.UpdateFormModel(pathToFormModel, formMarkup);
            
            var updatedContent = File.ReadAllText(pathToFormModel);
            Assert.Contains(formMarkup, updatedContent);

            //revert
            File.WriteAllText(pathToFormModel, originalContent);
            var revertedContent = File.ReadAllText(pathToFormModel);
            Assert.DoesNotContain(formMarkup, revertedContent);
        }
    }
}
