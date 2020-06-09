using System;
using System.Globalization;
using System.IO;
using PDFiller.Domain.FileFinders;
using PDFiller.Domain.FileUpdaters;
using Xunit;

namespace PDFiller.Domain.Tests.FileUpdaters
{
    public class HtmlFormUpdaterTests
    {
        [Fact]
        public void UpdateHtmlFormInBlazorProject_PathToHtmlFormNotProvided_ShouldThrow()
        {
            var htmlFormUpdater = new HtmlFormUpdater();
            Action action = () => htmlFormUpdater.UpdateHtmlFormInBlazorProject(string.Empty, string.Empty);

            var exception = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Path to HTML form not provided.", exception.Message);
        }

        [Fact]
        public void CopyPdfFormToBlazorProject_InvalidPathToPdfFormSource_ShouldThrow()
        {
            var htmlFormUpdater = new HtmlFormUpdater();
            Action action = () => htmlFormUpdater.UpdateHtmlFormInBlazorProject(@"C:\IShouldNotExist.razor", string.Empty);

            var exception = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Invalid path to HTML form.", exception.Message);
        }

        [Fact]
        public void UpdateHtmlFormInBlazorProject_FormMarkupNotProvided_ShouldThrow()
        {
            var htmlFormFinder = new HtmlFormFinder(AppDomain.CurrentDomain.BaseDirectory);
            var pathToHtmlForm = htmlFormFinder.GetPath();
            var htmlFormUpdater = new HtmlFormUpdater();
            Action action = () => htmlFormUpdater.UpdateHtmlFormInBlazorProject(pathToHtmlForm, string.Empty);

            var exception = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Form markup is null or empty.", exception.Message);
        }

        [Fact]
        public void CanUpdateHtmlForm()
        {
            var htmlFormFinder = new HtmlFormFinder(AppDomain.CurrentDomain.BaseDirectory);
            var pathToHtmlForm = htmlFormFinder.GetPath();
            var formMarkup = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);

            var htmlFormUpdater = new HtmlFormUpdater();
            
            var originalHtmlFormContent = File.ReadAllText(pathToHtmlForm);
            Assert.DoesNotContain(formMarkup, originalHtmlFormContent);

            htmlFormUpdater.UpdateHtmlFormInBlazorProject(pathToHtmlForm, formMarkup);
            var newHtmlFormContent = File.ReadAllText(pathToHtmlForm);
            Assert.Contains(formMarkup, newHtmlFormContent);

            //revert
            File.WriteAllText(pathToHtmlForm, originalHtmlFormContent);
            var cleanedUpHtmlFormContent = File.ReadAllText(pathToHtmlForm);
            Assert.DoesNotContain(formMarkup, cleanedUpHtmlFormContent);
        }
    }
}
