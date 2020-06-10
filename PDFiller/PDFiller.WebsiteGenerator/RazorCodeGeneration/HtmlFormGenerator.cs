using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PDFiller.Domain;
using PDFiller.WebsiteGenerator.RazorCodeGeneration.Fragments;

namespace PDFiller.WebsiteGenerator.RazorCodeGeneration
{
    public class HtmlFormGenerator
    {
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
                formMarkup.AppendLine(FormFragments.FormGroupStart());
                var labelMarkup = formField.Label();
                formMarkup.AppendLine(labelMarkup);

                if (formField.FieldType == FormFieldType.TextBox || formField.FieldType == FormFieldType.Unknown)
                {
                    var fieldMarkup = formField.TextBox();
                    formMarkup.AppendLine(fieldMarkup);
                    var helpMarkup = formField.FieldHelp();
                    formMarkup.AppendLine(helpMarkup);
                }
                else if (formField.FieldType == FormFieldType.CheckBox)
                {
                    var fieldMarkup = formField.CheckBox();
                    formMarkup.AppendLine(fieldMarkup);
                }
                formMarkup.AppendLine(FormFragments.FormGroupEnd());
                formMarkup.AppendLine();
            }

            return formMarkup.ToString().TrimEnd(Environment.NewLine.ToCharArray());
        }
    }
}
