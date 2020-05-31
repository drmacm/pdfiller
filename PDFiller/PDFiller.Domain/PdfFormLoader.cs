using Common.Logging.Factory;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
using iText.Layout;
using System.Collections.Generic;

namespace PDFiller.Domain
{
    public static class PdfFormLoader
    {
        public static IDictionary<string, PdfFormField> GetFormFields(string filename)
        {
            var pdfDocument = new PdfDocument(new PdfReader(filename));
            var document = new Document(pdfDocument);
            var form = PdfAcroForm.GetAcroForm(pdfDocument, false);

            var formFields = form.GetFormFields();
            document.Close();

            return formFields;
        }
    }
}
