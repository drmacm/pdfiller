using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.CodeAnalysis.CSharp;
using PDFiller.Domain;

namespace PDFiller.CSharpCodeGeneration
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

            var formModelPropertyGenerator = new FormFieldPropertyGenerator(formFields);

            var formModelContent = File.ReadAllText(pathToFormModel);
            var syntaxTree = CSharpSyntaxTree.ParseText(formModelContent);

            var modifiedSyntaxTree = formModelPropertyGenerator.Visit(syntaxTree.GetRoot());
                
            var updatedFormModelContent = modifiedSyntaxTree.ToFullString();
            return updatedFormModelContent;
        }
    }
}