using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PDFiller.Models;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace PDFiller.WebsiteGenerator.CSharpCodeGeneration
{
    public class FormFieldPropertyGenerator : CSharpSyntaxRewriter
    {
        private readonly string _twoLevelIndentation;
        private readonly List<FormField> _formFields;

        public FormFieldPropertyGenerator(List<FormField> formFields)
        {
            _formFields = formFields ?? new List<FormField>();
            _twoLevelIndentation = new string(' ', 8);
        }

        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            var properties = new List<MemberDeclarationSyntax>();
            
            var isFirstProperty = true;
            foreach (var formField in _formFields)
            {
                properties.Add(Property(formField, isFirstProperty));
                isFirstProperty = false;
            }

            return node.WithMembers(
                List(properties)
            );
        }

        /// <summary>
        /// Generates a property based on a <see cref="FormField"/>.
        /// </summary>
        private MemberDeclarationSyntax Property(FormField formField, bool isFirstProperty)
        {
            var attributes = Attributes(formField, isFirstProperty);     // [Required]
            var modifiers = Modifiers();                // public
            var type = Type(formField);                 // string
            var identifier = Identifier(formField);     // Foo
            var accessors = Accessors();                // { get; set; }

            var propertyDeclaration =
                    PropertyDeclaration(type, identifier)
                        .WithAttributeLists(attributes)
                        .WithModifiers(modifiers)
                        .WithAccessorList(accessors)
                ;

            return propertyDeclaration;
        }

        /// <summary>
        /// Generates property attributes, such as "[Required]".
        /// </summary>
        private SyntaxList<AttributeListSyntax> Attributes(FormField formField, bool isFirstProperty)
        {
            var attributes = new List<AttributeListSyntax>();

            if (formField.IsRequired)
            {
                var requiredAttribute = RequiredAttribute(isFirstProperty);
                attributes.Add(requiredAttribute);
            }

            return List(attributes);
        }

        /// <summary>
        /// Generates the "[Required]" attribute.
        /// </summary>
        private AttributeListSyntax RequiredAttribute(bool isFirstProperty)
        {
            var attribute = Attribute(
                IdentifierName("Required")
            );

            var openingTrivia = isFirstProperty
                ? TriviaList(Whitespace(_twoLevelIndentation))
                : TriviaList(CarriageReturnLineFeed, Whitespace(_twoLevelIndentation));

            var openingBracket = Token(
                openingTrivia,
                SyntaxKind.OpenBracketToken,
                TriviaList()
            );

            var closingBracket = Token(
                TriviaList(),
                SyntaxKind.CloseBracketToken,
                TriviaList(CarriageReturnLineFeed)
            );

            var attributeList = AttributeList(
                    SingletonSeparatedList(attribute)
                )
                .WithOpenBracketToken(openingBracket)
                .WithCloseBracketToken(closingBracket);

            return attributeList;
        }

        /// <summary>
        /// Generates the "public " part of the property (with indentation).
        /// </summary>
        private SyntaxTokenList Modifiers()
        {
            var propertyModifiers = TokenList(
                Token(
                    TriviaList(Whitespace(_twoLevelIndentation)),
                    SyntaxKind.PublicKeyword,
                    TriviaList(Space)
                )
            );

            return propertyModifiers;
        }

        /// <summary>
        /// Generates the type of property, based on <see cref="FormField.FieldType"/>.
        /// </summary>
        private TypeSyntax Type(FormField formField)
        {
            var propertyTypeKeyword = formField.FieldType switch
            {
                FormFieldType.TextBox => SyntaxKind.StringKeyword,
                FormFieldType.CheckBox => SyntaxKind.BoolKeyword,
                _ => SyntaxKind.StringKeyword
            };

            var propertyType = PredefinedType(
                Token(
                    TriviaList(),
                    propertyTypeKeyword,
                    TriviaList(Space)
                )
            );

            return propertyType;
        }

        /// <summary>
        /// Generates the name of the property based on the <see cref="FormField.CSharpFieldName"/>.
        /// </summary>
        private SyntaxToken Identifier(FormField formField)
        {
            var propertyIdentifier = SyntaxFactory.Identifier(
                TriviaList(),
                formField.CSharpFieldName,
                TriviaList(Space)
            );

            return propertyIdentifier;
        }

        /// <summary>
        /// Generates the accessors for the property ("{ get; set; }" part).
        /// </summary>
        private AccessorListSyntax Accessors()
        {
            var openingBrace = Token(
                TriviaList(),
                SyntaxKind.OpenBraceToken,
                TriviaList(Space)
            );

            var closingBrace = Token(
                TriviaList(),
                SyntaxKind.CloseBraceToken,
                TriviaList(CarriageReturnLineFeed)
            );

            var semicolon = Token(
                TriviaList(),
                SyntaxKind.SemicolonToken,
                TriviaList(Space)
            );

            var getAccessor = AccessorDeclaration(
                SyntaxKind.GetAccessorDeclaration
            );

            var setAccessor = AccessorDeclaration(
                SyntaxKind.SetAccessorDeclaration
            );

            var propertyAccessors = AccessorList(
                List(
                    new []
                    {
                        getAccessor
                            .WithSemicolonToken(semicolon),
                        setAccessor
                            .WithSemicolonToken(semicolon)
                    }
                )
            )
                .WithOpenBraceToken(openingBrace)
                .WithCloseBraceToken(closingBrace);

            return propertyAccessors;
        }
    }
}