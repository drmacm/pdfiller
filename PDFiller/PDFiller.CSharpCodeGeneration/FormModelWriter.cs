using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PDFiller.Domain;

namespace PDFiller.CSharpCodeGeneration
{
    public class FormModelWriter : CSharpSyntaxRewriter
    {
        private readonly List<FormField> _formFields;

        public FormModelWriter(List<FormField> formFields)
        {
            _formFields = formFields;
        }

        public override SyntaxNode? VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            //SyntaxFactory.ClassDeclaration()
            return base.VisitClassDeclaration(node);
        }
    }
}
