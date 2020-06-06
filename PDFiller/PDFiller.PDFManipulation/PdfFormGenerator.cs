using System.Collections.Generic;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using PDFiller.Domain;

namespace PDFiller.PDFManipulation
{
    public static class PdfFormGenerator
    {
        public static void GenerateForm(string filename, IEnumerable<FormField> formFields)
        {
            var pdfDocument = new PdfDocument(new PdfWriter(filename));
            var document = new Document(pdfDocument);
            var form = PdfAcroForm.GetAcroForm(pdfDocument, true);

            var xPosition = 20;
            var yPosition = 800;
            var fieldSpacingHeight = 20;
            foreach (var formField in formFields)
            {
                if (formField.FieldType == FormFieldType.TextBox)
                {
                    var width = 100;
                    var height = 20;

                    var rectangle = new Rectangle(xPosition, yPosition, width, height);
                    var field = PdfFormField.CreateText(document.GetPdfDocument(), rectangle, formField.FieldName, "");
                    form.AddField(field);

                    yPosition -= height;
                }
                if (formField.FieldType == FormFieldType.CheckBox)
                {
                    var width = 20;
                    var height = 20;

                    var rectangle = new Rectangle(xPosition, yPosition, width, height);
                    var field = PdfFormField.CreateCheckBox(document.GetPdfDocument(), rectangle, formField.FieldName, "");
                    form.AddField(field);

                    yPosition -= height;
                }
                yPosition -= fieldSpacingHeight;
            }
            document.Close();
        }
    }
}
