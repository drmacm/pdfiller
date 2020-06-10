using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Components;
using PDFiller.Domain;
using PDFiller.WebsiteGenerator.RazorCodeGeneration.Fragments;

namespace PDFiller.WebsiteGenerator.RazorCodeGeneration
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
            var labelMarkup = Render(formField.Label());
            var textInputMarkup = Render(formField.TextBox());

            var markup = string.Join(Environment.NewLine, labelMarkup, textInputMarkup);

            return markup;
        }

        private string RenderCheckboxInputWithLabel(FormField formField)
        {
            var labelMarkup = Render(formField.Label());
            var checkboxInputMarkup = Render(formField.CheckBox());

            var markup = string.Join(Environment.NewLine, labelMarkup, checkboxInputMarkup);

            return markup;
        }

        private string Render(RenderFragment fragmentBuilder)
        {
            return _fragmentRenderer.Render(fragmentBuilder);
        }
    }
}
