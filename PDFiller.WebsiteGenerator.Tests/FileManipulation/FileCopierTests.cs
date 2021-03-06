﻿using System;
using System.IO;
using PDFiller.WebsiteGenerator.FileManipulation;
using PDFiller.WebsiteGenerator.FileManipulation.FileFinders;
using Xunit;

namespace PDFiller.WebsiteGenerator.Tests.FileManipulation
{
    public class FileCopierTests
    {
        [Fact]
        public void CopyPdfFormToBlazorProject_PdfFormSourceNotProvided_ShouldThrow()
        {
            Action action = () => FileCopier.CopyPdfFormToBlazorProject(string.Empty, string.Empty);

            var exception = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Path to source PDF form not provided.", exception.Message);
        }

        [Fact]
        public void CopyPdfFormToBlazorProject_InvalidPathToPdfFormSource_ShouldThrow()
        {
            Action action = () => FileCopier.CopyPdfFormToBlazorProject(@"C:\IShouldNotExist.pdf", string.Empty);

            var exception = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Invalid path to source PDF form.", exception.Message);
        }

        [Fact]
        public void CanCopyPdfFormFile()
        {
            var pdfFormFinder = new PdfFormFinder(AppDomain.CurrentDomain.BaseDirectory);
            var pdfFormDestination = pdfFormFinder.GetPath();
            if (File.Exists(pdfFormDestination))
            {
                File.Delete(pdfFormDestination);
            }

            Assert.False(File.Exists(pdfFormDestination));

            var pdfFormSource = @"FileManipulation\SamplePDFs\PRP-1-bos.pdf";
            FileCopier.CopyPdfFormToBlazorProject(pdfFormSource, pdfFormDestination);

            Assert.True(File.Exists(pdfFormDestination));
        }
    }
}
