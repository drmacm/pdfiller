using PDFiller.PDFManipulation;
using System;
using System.Collections.Generic;
using PDFiller.Domain;
using PDFiller.Domain.FileFinders;
using PDFiller.RazorCodeGeneration;

namespace PDFiller.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var applicationFolder = AppDomain.CurrentDomain.BaseDirectory;
            var pdfFormFinder = new PdfFormFinder(applicationFolder);
            var htmlFormFinder = new HtmlFormFinder(applicationFolder);

            var pathToPdfForm = pdfFormFinder.GetPath();
            var pathToHtmlForm = htmlFormFinder.GetPath();
            
            CopyPdfFormToWebsite(pathToPdfForm);

            var formFields = GetFormFields(pathToPdfForm);
            var filteredFormFields = FilterFormFields(formFields);
            
            var formMarkup = GenerateHtmlForFormFields(filteredFormFields);

            UpdateHtmlForm(formMarkup, pathToHtmlForm);
        }

        private static void CopyPdfFormToWebsite(string pathToPdfForm)
        {
            Console.WriteLine("Enter a path to the PDF form:");
            var pdfFormSource = Console.ReadLine();
            if (string.IsNullOrEmpty(pdfFormSource))
            {
                //pdfFormSource = @"SamplePDFs\TextBoxForm.pdf";
                pdfFormSource = @"SamplePDFs\PRP-1-bos.pdf";
            }

            Console.WriteLine("Copying the file:");
            Console.WriteLine(@$"""{pdfFormSource}""");
            Console.WriteLine("To:");
            Console.WriteLine(@$"""{pathToPdfForm}""");

            FileCopier.CopyPdfFormToBlazorProject(pdfFormSource, pathToPdfForm);
        }

        private static List<FormField> GetFormFields(string pathToPdfForm)
        {
            Console.WriteLine();
            Console.WriteLine("Extracting form fields from the PDF file.");
            var formFields = PdfFormLoader.GetFormFields(pathToPdfForm);

            return formFields;
        }

        private static List<FormField> FilterFormFields(List<FormField> formFields)
        {
            Console.WriteLine();
            Console.WriteLine("Form fields: ");

            var filteredFormFields = new List<FormField>();
            foreach (var formField in formFields)
            {
                Console.WriteLine($@"""{formField.FieldName}"" - (s)skip, (o)optional, (r or ENTER)required?");
                var shouldMoveOn = false;
                while (!shouldMoveOn)
                {
                    var pressedKey = Console.ReadKey(true);

                    if (pressedKey.Key == ConsoleKey.S)
                    {
                        shouldMoveOn = true;
                    }

                    if (pressedKey.Key == ConsoleKey.O)
                    {
                        formField.MakeOptional();
                        filteredFormFields.Add(formField);
                        shouldMoveOn = true;
                    }

                    if (pressedKey.Key == ConsoleKey.O || pressedKey.Key == ConsoleKey.R || pressedKey.Key == ConsoleKey.Enter)
                    {
                        filteredFormFields.Add(formField);
                        shouldMoveOn = true;
                    }
                }
            }

            return filteredFormFields;
        }

        private static string GenerateHtmlForFormFields(List<FormField> formFields)
        {
            var htmlFormGenerator = new HtmlFormGenerator(new FragmentRenderer());
            var formMarkup = htmlFormGenerator.GenerateForm(formFields);

            return formMarkup;
        }

        private static void UpdateHtmlForm(string formMarkup, string pathToHtmlForm)
        {
            var htmlFormUpdater = new HtmlFormUpdater();
            htmlFormUpdater.UpdateHtmlFormInBlazorProject(pathToHtmlForm, formMarkup);
        }
    }
}
