using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using PDFiller.Domain;
using PDFiller.RazorCodeGeneration.Utility;

namespace PDFiller.RazorCodeGeneration
{
    public class HtmlFormGenerator
    {
        private readonly IFragmentRenderer _fragmentRenderer;

        public HtmlFormGenerator(IFragmentRenderer fragmentRenderer)
        {
            _fragmentRenderer = fragmentRenderer;
        }

        public string GenerateForm(List<FormField> formFields)
        {
            if (formFields == null)
            {
                throw new ArgumentException("List of form fields can't be null.");
            }

            if (!formFields.Any())
            {
                return string.Empty;
            }

            var formMarkup = new StringBuilder();
            foreach (var formField in formFields)
            {
                var fieldMarkup = string.Empty;
                if (formField.FieldType == FormFieldType.TextBox)
                {
                    fieldMarkup = RenderTextInputWithLabel(formField);
                }
                if (formField.FieldType == FormFieldType.CheckBox)
                {
                    fieldMarkup = RenderCheckboxInputWithLabel(formField);
                }

                formMarkup.Append(fieldMarkup);
                formMarkup.AppendLine();
                formMarkup.AppendLine();
            }

            return formMarkup.ToString().TrimEnd(Environment.NewLine.ToCharArray());
        }

        private string RenderTextInputWithLabel(FormField formField)
        {
            var labelMarkup = RenderLabel(formField);
            var textInputMarkup = RenderTextInput(formField);

            var markup = string.Join(Environment.NewLine, labelMarkup, textInputMarkup);

            return markup;
        }

        private string RenderCheckboxInputWithLabel(FormField formField)
        {
            var labelMarkup = RenderLabel(formField);
            var checkboxInputMarkup = RenderCheckboxInput(formField);

            var markup = string.Join(Environment.NewLine, labelMarkup, checkboxInputMarkup);

            return markup;
        }

        private string RenderLabel(FormField formField)
        {
            void Label(RenderTreeBuilder builder)
            {
                builder.OpenElement(0, "label");
                builder.AddAttribute(0, "for", formField.FieldName);
                builder.AddContent(0, formField.FieldName);
                builder.CloseElement();
            }

            return _fragmentRenderer.Render(Label);
        }

        private string RenderTextInput(FormField formField)
        {
            void TextInput(RenderTreeBuilder builder)
            {
                builder.OpenElement(0, "input");
                builder.AddAttribute(0, "id", formField.FieldName);
                builder.AddAttribute(0, "name", formField.FieldName);
                builder.AddAttribute(0, "required", "required");
                builder.CloseElement();
            }

            return _fragmentRenderer.Render(TextInput);
        }

        private string RenderCheckboxInput(FormField formField)
        {
            void TextInput(RenderTreeBuilder builder)
            {
                builder.OpenElement(0, "input");
                builder.AddAttribute(0, "id", formField.FieldName);
                builder.AddAttribute(0, "name", formField.FieldName);
                builder.AddAttribute(0, "type", "checkbox");
                builder.AddAttribute(0, "required", "required");
                builder.CloseElement();
            }

            return _fragmentRenderer.Render(TextInput);
        }
    }
}
