using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using System;

namespace PDFiller.Domain
{
    //https://itextpdf.com/en/resources/books/itext-7-jump-start-tutorial-net/chapter-4-making-pdf-interactive
    public class PdfFormGenerator
    {
        private readonly Document _document;
        private readonly PdfAcroForm _form;

        /// <summary>
        /// Creates a new <see cref="PdfFormGenerator"/>.
        /// </summary>
        /// <param name="filename">Filename of a resulting PDF form.</param>
        public PdfFormGenerator(string filename) 
        {
            var pdfWriter = new PdfWriter(filename);
            var pdfDocument = new PdfDocument(pdfWriter);
            _document = new Document(pdfDocument);
            _form = PdfAcroForm.GetAcroForm(pdfDocument, true);
        }

        public void GenerateFormWithSingleTextField() 
        {
            var nameField = PdfTextFormField.CreateText(_document.GetPdfDocument(), new Rectangle(99, 753, 425, 15), "name", "");
            _form.AddField(nameField);
            _document.Close();
        }
    }
}
