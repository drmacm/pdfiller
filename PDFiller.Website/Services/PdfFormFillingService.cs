using System.Collections.Generic;
using System.IO;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
using iText.Layout;
using PDFiller.Website.Models;

namespace PDFiller.Website.Services
{
    public class PdfFormFillingService
    {
        private IDictionary<string, PdfFormField> _formFields;

        public MemoryStream FillForm(MemoryStream formToFill, FormModel model)
        {
            var writerStream = new MemoryStream();

            var pdfReader = new PdfReader(formToFill);
            var pdfWriter = new PdfWriter(writerStream);
            var pdfDocument = new PdfDocument(pdfReader, pdfWriter);
            var document = new Document(pdfDocument);
            var form = PdfAcroForm.GetAcroForm(pdfDocument, true);

            _formFields = form.GetFormFields();
            FillFormFields(model);

            document.Close();

            return writerStream;
        }

        private void FillFormFields(FormModel model)
        {
        }

        private void FillFormField(string fieldName, string value)
        {
            if (_formFields.ContainsKey(fieldName) && !string.IsNullOrEmpty(value))
            {
                var formField = _formFields[fieldName];
                formField.SetValue(value);
            }
        }

        private void FillFormField(string fieldName, bool value)
        {
            if (_formFields.ContainsKey(fieldName) && value)
            {
                var formField = _formFields[fieldName];
                formField.SetValue(value.ToString());
            }
        }
    }
}
