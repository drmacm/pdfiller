using System;
using System.IO;

namespace PDFiller.Domain.FileUpdaters
{
    public class HtmlFormUpdater
    {
        private readonly string _placeholder = "<!--[FORM_FIELDS_PLACEHOLDER]-->";
        
        public void UpdateHtmlFormInBlazorProject(string pathToHtmlForm, string formMarkup)
        {
            if (string.IsNullOrEmpty(pathToHtmlForm))
            {
                throw new ArgumentException("Path to HTML form not provided.");
            }
            if (!File.Exists(pathToHtmlForm))
            {
                throw new ArgumentException("Invalid path to HTML form.");
            }

            if (string.IsNullOrEmpty(formMarkup))
            {
                throw new ArgumentException("Form markup is null or empty.");
            }

            var existingFormMarkup = File.ReadAllText(pathToHtmlForm);

            var updatedFormMarkup = existingFormMarkup.Replace(_placeholder, _placeholder + Environment.NewLine + formMarkup);

            File.WriteAllText(pathToHtmlForm, updatedFormMarkup);
        }
    }
}
