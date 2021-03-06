﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using PDFiller.Models;
using PDFiller.WebsiteGenerator.CSharpCodeGeneration;
using PDFiller.WebsiteGenerator.FileManipulation;
using PDFiller.WebsiteGenerator.FileManipulation.FileFinders;
using PDFiller.WebsiteGenerator.FileManipulation.FileUpdaters;
using PDFiller.WebsiteGenerator.PDFManipulation;
using PDFiller.WebsiteGenerator.RazorCodeGeneration;

namespace PDFiller.WebsiteGenerator.ConsoleRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var applicationFolder = AppDomain.CurrentDomain.BaseDirectory;
            var pdfFormFinder = new PdfFormFinder(applicationFolder);
            var htmlFormFinder = new HtmlFormFinder(applicationFolder);
            var formModelFinder = new FormModelFinder(applicationFolder);
            var pdfFormFillingServiceFinder = new PdfFormFillingServiceFinder(applicationFolder);

            var pathToPdfForm = pdfFormFinder.GetPath();
            var pathToHtmlForm = htmlFormFinder.GetPath();
            var pathToFormModel = formModelFinder.GetPath();
            var pathToPdfFormFillingService = pdfFormFillingServiceFinder.GetPath();

            CopyPdfFormToWebsite(pathToPdfForm);

            var formFields = GetFormFields(pathToPdfForm);
            var filteredFormFields = FilterFormFields(formFields);
            
            var formMarkup = GenerateHtmlForFormFields(filteredFormFields);

            UpdateHtmlForm(formMarkup, pathToHtmlForm);

            var formModelContent = GenerateFormModel(formFields, pathToFormModel);

            UpdateFormModel(pathToFormModel, formModelContent);

            var pdfFormFillingServiceContent = GeneratePdfFormFillingService(formFields, pathToPdfFormFillingService);

            UpdatePdfFormFillingService(pathToPdfFormFillingService, pdfFormFillingServiceContent);
        }

        private static void CopyPdfFormToWebsite(string pathToPdfForm)
        {
            Console.WriteLine("PDFiller - a tool that helps you create a simple static website based on your fillable PDF form.");
            Console.WriteLine();
            Console.WriteLine("Enter a path to the PDF form:");
            var pdfFormSource = Console.ReadLine();
            if (string.IsNullOrEmpty(pdfFormSource) || !File.Exists(pdfFormSource))
            {
                Console.WriteLine("You have to provide a valid path to a PDF form");
                Environment.Exit(0);
            }

            Console.WriteLine("Copying PDF form to the website:");
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
                    //This is used as a shortcut to accept all fields as required, to speed up testing
                    //var pressedKey = new ConsoleKeyInfo('r', ConsoleKey.R, false, false, false);

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

                    if (pressedKey.Key == ConsoleKey.R || pressedKey.Key == ConsoleKey.Enter)
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
            var htmlFormGenerator = new HtmlFormGenerator();
            var formMarkup = htmlFormGenerator.GenerateForm(formFields);

            return formMarkup;
        }

        private static void UpdateHtmlForm(string formMarkup, string pathToHtmlForm)
        {
            var htmlFormUpdater = new HtmlFormUpdater();
            htmlFormUpdater.UpdateHtmlFormInBlazorProject(pathToHtmlForm, formMarkup);
        }

        private static string GenerateFormModel(List<FormField> formFields, string pathToFormModel)
        {
            var formModelGenerator = new FormModelGenerator();
            var formModelContent = formModelGenerator.Generate(formFields, pathToFormModel);

            return formModelContent;
        }

        private static void UpdateFormModel(string pathToFormModel, string formModelContent)
        {
            var formModelUpdater = new FormModelUpdater();
            formModelUpdater.UpdateFormModel(pathToFormModel, formModelContent);
        }

        private static string GeneratePdfFormFillingService(List<FormField> formFields, string pathToPdfFormFillingService)
        {
            var pdfFormFillingServiceGenerator = new PdfFormFillingServiceGenerator();
            var pdfFormFillingServiceContent = pdfFormFillingServiceGenerator.Generate(formFields, pathToPdfFormFillingService);

            return pdfFormFillingServiceContent;
        }

        private static void UpdatePdfFormFillingService(string pathToPdfFormFillingService, string pdfFormFillingServiceContent)
        {
            var pdfFormFillingServiceUpdater = new PdfFormFillingServiceUpdater();
            pdfFormFillingServiceUpdater.Update(pathToPdfFormFillingService, pdfFormFillingServiceContent);
        }
    }
}
