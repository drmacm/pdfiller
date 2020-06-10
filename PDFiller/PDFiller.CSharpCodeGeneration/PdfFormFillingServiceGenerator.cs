using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.CodeAnalysis.CSharp;
using PDFiller.Domain;

namespace PDFiller.CSharpCodeGeneration
{
    public class PdfFormFillingServiceGenerator
    {
        public string Generate(List<FormField> formFields, string pathToPdfFormFillingService)
        {
            if (string.IsNullOrEmpty(pathToPdfFormFillingService))
            {
                throw new ArgumentException("Path to Pdf form filling service class not provided.");
            }
            if (!File.Exists(pathToPdfFormFillingService))
            {
                throw new ArgumentException("Invalid path to Pdf form filling service class.");
            }

            var pdfFormFillingServiceMethodCallGenerator = new PdfFormFillingServiceMethodCallGenerator(formFields);

            var formModelContent = File.ReadAllText(pathToPdfFormFillingService);
            var syntaxTree = CSharpSyntaxTree.ParseText(formModelContent);

            var modifiedSyntaxTree = pdfFormFillingServiceMethodCallGenerator.Visit(syntaxTree.GetRoot());
                
            var updatedPdfFormFillingServiceContent = modifiedSyntaxTree.ToFullString();
            return updatedPdfFormFillingServiceContent;
        }
    }
}