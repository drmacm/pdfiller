using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.CodeAnalysis.CSharp;
using PDFiller.Models;

namespace PDFiller.WebsiteGenerator.CSharpCodeGeneration
{
    public class FormModelGenerator
    {
        public string Generate(List<FormField> formFields, string pathToFormModel)
        {
            if (string.IsNullOrEmpty(pathToFormModel))
            {
                throw new ArgumentException("Path to FormModel class not provided.");
            }
            if (!File.Exists(pathToFormModel))
            {
                throw new ArgumentException("Invalid path to FormModel class.");
            }

            var codeGenerator = new FormFieldPropertyGenerator(formFields);

            var originalContent = File.ReadAllText(pathToFormModel);
            var syntaxTree = CSharpSyntaxTree.ParseText(originalContent);

            var modifiedSyntaxTree = codeGenerator.Visit(syntaxTree.GetRoot());
                
            var updatedContent = modifiedSyntaxTree.ToFullString();
            return updatedContent;
        }
    }
}