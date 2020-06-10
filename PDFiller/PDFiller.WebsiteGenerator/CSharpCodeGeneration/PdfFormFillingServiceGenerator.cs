using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.CodeAnalysis.CSharp;
using PDFiller.Domain;

namespace PDFiller.WebsiteGenerator.CSharpCodeGeneration
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

            var codeGenerator = new PdfFormFillingServiceMethodCallGenerator(formFields);

            var originalContent = File.ReadAllText(pathToPdfFormFillingService);
            var syntaxTree = CSharpSyntaxTree.ParseText(originalContent);

            var modifiedSyntaxTree = codeGenerator.Visit(syntaxTree.GetRoot());
                
            var updatedContent = modifiedSyntaxTree.ToFullString();
            return updatedContent;
        }
    }
}