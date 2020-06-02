using System.Collections.Generic;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
using iText.Layout;
using PDFiller.Domain;

namespace PDFiller.PDFManipulation
{
    public static class PdfFormLoader
    {
        public static List<FormField> GetFormFields(string filename)
        {
            var pdfDocument = new PdfDocument(new PdfReader(filename));
            var document = new Document(pdfDocument);
            var form = PdfAcroForm.GetAcroForm(pdfDocument, false);

            var pdfFormFields = form.GetFormFields();

            var formFields = new List<FormField>();
            foreach (var pdfFormField in pdfFormFields)
            {
                var fieldType = FormFieldType.Unknown;
                if (pdfFormField.Value is PdfTextFormField)
                {
                    fieldType = FormFieldType.TextBox;
                }
                if (pdfFormField.Value is PdfButtonFormField)
                {
                    fieldType = FormFieldType.CheckBox;
                }
                formFields.Add(new FormField(pdfFormField.Key, fieldType));
            }

            document.Close();

            return formFields;
        }
    }
}
