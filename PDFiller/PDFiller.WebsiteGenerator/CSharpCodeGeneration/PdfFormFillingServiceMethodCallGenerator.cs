using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PDFiller.Domain;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace PDFiller.WebsiteGenerator.CSharpCodeGeneration
{
    public class PdfFormFillingServiceMethodCallGenerator : CSharpSyntaxRewriter
    {
        private readonly string _methodNameToUpdate;
        private readonly string _methodNameToCall;
        private readonly string _formModelVariableName;

        private readonly string _twoLevelIndentation;
        private readonly string _threeLevelIndentation;

        private readonly List<FormField> _formFields;

        public PdfFormFillingServiceMethodCallGenerator(List<FormField> formFields)
        {
            _methodNameToUpdate = "FillFormFields";
            _methodNameToCall = "FillFormField";
            _formModelVariableName = "model";

            _formFields = formFields ?? new List<FormField>();
            _twoLevelIndentation = new string(' ', 8);
            _threeLevelIndentation = new string(' ', 12);
        }

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (node.Identifier.ValueText != _methodNameToUpdate)
            {
                return base.VisitMethodDeclaration(node);
            }

            var methodBody = MethodBody(_formFields);

            return node
                .WithBody(methodBody);

        }

        private BlockSyntax MethodBody(List<FormField> formFields)
        {
            var openingBrace = Token(
                TriviaList(Whitespace(_twoLevelIndentation)),
                SyntaxKind.OpenBraceToken,
                TriviaList(CarriageReturnLineFeed)
            );

            var closingBrace = Token(
                TriviaList(Whitespace(_twoLevelIndentation)),
                SyntaxKind.CloseBraceToken,
                TriviaList(CarriageReturnLineFeed)
            );

            var methodCalls = new List<ExpressionStatementSyntax>();
            foreach (var formField in _formFields)
            {
                var methodCall = MethodCall(formField);
                methodCalls.Add(methodCall);
            }

            var methodBody = Block(
                    List<StatementSyntax>(methodCalls)
                )
                .WithOpenBraceToken(openingBrace)
                .WithCloseBraceToken(closingBrace);

            return methodBody;
        }


        private ExpressionStatementSyntax MethodCall(FormField formField)
        {
            var identifier = Identifier(
                TriviaList(Whitespace(_threeLevelIndentation)),
                _methodNameToCall,
                TriviaList()
            );

            var arguments = MethodCallArguments(formField);

            var semicolon = Token(
                TriviaList(),
                SyntaxKind.SemicolonToken,
                TriviaList(CarriageReturnLineFeed)
            );

            var invocationExpression = InvocationExpression(
                    IdentifierName(identifier)
                )
                    .WithArgumentList(arguments);

            var expressionStatement = ExpressionStatement(
                    invocationExpression
                )
                    .WithSemicolonToken(semicolon);
            
            return expressionStatement;
        }

        private ArgumentListSyntax MethodCallArguments(FormField formField)
        {
            var firstArgument = Argument(
                LiteralExpression(
                    SyntaxKind.StringLiteralExpression,
                    Literal(formField.FieldName)
                )
            );

            var comma = Token(
                TriviaList(),
                SyntaxKind.CommaToken,
                TriviaList(Space)
            );

            var secondArgument = Argument(
                MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    IdentifierName(_formModelVariableName),
                    IdentifierName(formField.CSharpFieldName)
                )
            );

            var methodCallArguments = ArgumentList(
                SeparatedList<ArgumentSyntax>(
                    new SyntaxNodeOrToken[]
                    {
                        firstArgument, comma, secondArgument
                    }
                )
            );

            return methodCallArguments;
        }
    }
}