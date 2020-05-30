using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
using iText.Layout;
using System.Collections.Generic;

namespace PDFiller.Domain
{
    public class PdfFormLoader
    {
        private readonly Document _document;
        private readonly PdfAcroForm _form;

        /// <summary>
        /// Creates a new <see cref="PdfFormLoader"/>.
        /// </summary>
        /// <param name="filename">Filename of a PDF form to load.</param>
        public PdfFormLoader(string filename)
        {
            var pdfReader = new PdfReader(filename);
            var pdfDocument = new PdfDocument(pdfReader);
            _document = new Document(pdfDocument);
            _form = PdfAcroForm.GetAcroForm(pdfDocument, false);
        }

        public IDictionary<string, PdfFormField> GetFormFields()
        {
            var formFields = _form.GetFormFields();
            _document.Close();

            return formFields;
        }
    }
}
