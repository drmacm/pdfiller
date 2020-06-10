using System;
using System.Globalization;
using System.IO;
using PDFiller.Domain.FileFinders;
using PDFiller.Domain.FileUpdaters;
using Xunit;

namespace PDFiller.Domain.Tests.FileUpdaters
{
    public class PdfFormFillingServiceUpdaterTests
    {
        [Fact]
        public void Update_PathToPdfFormFillingServiceNotProvided_ShouldThrow()
        {
            var pdfFormFillingServiceUpdater = new PdfFormFillingServiceUpdater();
            Action action = () => pdfFormFillingServiceUpdater.Update(string.Empty, string.Empty);

            var exception = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Path to Pdf form filling service class not provided.", exception.Message);
        }

        [Fact]
        public void Update_InvalidPathToPdfFormFillingService_ShouldThrow()
        {
            var pdfFormFillingServiceUpdater = new PdfFormFillingServiceUpdater();
            Action action = () => pdfFormFillingServiceUpdater.Update(@"C:\IShouldNotExist.razor", string.Empty);

            var exception = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Invalid path to Pdf form filling service class.", exception.Message);
        }

        [Fact]
        public void UpdateFormModel_SourceCodeNotProvided_ShouldThrow()
        {
            var pdfFormFillingServiceFinder = new PdfFormFillingServiceFinder(AppDomain.CurrentDomain.BaseDirectory);
            var pathToFormModel = pdfFormFillingServiceFinder.GetPath();
            var pdfFormFillingServiceUpdater = new PdfFormFillingServiceUpdater();
            Action action = () => pdfFormFillingServiceUpdater.Update(pathToFormModel, string.Empty);

            var exception = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Source code is null or empty.", exception.Message);
        }

        [Fact]
        public void CanUpdatePdfFormFillingService()
        {
            var pdfFormFillingServiceFinder = new PdfFormFillingServiceFinder(AppDomain.CurrentDomain.BaseDirectory);
            var pathToFormModel = pdfFormFillingServiceFinder.GetPath();
            var methodCallToInsert = "Foo(bar, baz.bez);";

            var pdfFormFillingServiceUpdater = new PdfFormFillingServiceUpdater();

            var originalContent = File.ReadAllText(pathToFormModel);
            Assert.DoesNotContain(methodCallToInsert, originalContent);

            pdfFormFillingServiceUpdater.Update(pathToFormModel, methodCallToInsert);
            
            var updatedContent = File.ReadAllText(pathToFormModel);
            Assert.Contains(methodCallToInsert, updatedContent);

            //revert
            File.WriteAllText(pathToFormModel, originalContent);
            var revertedContent = File.ReadAllText(pathToFormModel);
            Assert.DoesNotContain(methodCallToInsert, revertedContent);
        }
    }
}
