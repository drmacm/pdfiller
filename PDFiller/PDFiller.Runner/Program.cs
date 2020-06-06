using PDFiller.PDFManipulation;
using System;
using System.IO;
using PDFiller.Domain;
using PDFiller.RazorCodeGeneration;

namespace PDFiller.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            CopyPdfFormToWebsite();
        }

        private static void CopyPdfFormToWebsite()
        {
            Console.WriteLine("Enter a path to the PDF form:");
            var pdfFormSource = Console.ReadLine();
            if (string.IsNullOrEmpty(pdfFormSource))
            {
                pdfFormSource = @"SamplePDFs\TextBoxForm.pdf";
            }

            var applicationFolder = AppDomain.CurrentDomain.BaseDirectory;
            PdfFormFinder pdfFormFinder = new PdfFormFinder(applicationFolder);
            var pdfFormDestination = pdfFormFinder.GetPathToPdfForm();
            
            FileCopier.CopyPdfFormToBlazorProject(pdfFormSource, pdfFormDestination);

            var formFields = PdfFormLoader.GetFormFields(pdfFormDestination);

            var htmlFormGenerator = new HtmlFormGenerator(new FragmentRenderer());
            var formMarkup = htmlFormGenerator.GenerateForm(formFields);

            var htmlFormFinder = new HtmlFormFinder(applicationFolder);
            var pathToHtmlForm = htmlFormFinder.GetPathToHtmlForm();
            
            var htmlFormUpdater = new HtmlFormUpdater();
            htmlFormUpdater.UpdateHtmlFormInBlazorProject(pathToHtmlForm, formMarkup);
        }
    }
}
