using System;
using System.Collections.Generic;
using System.IO;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
using iText.Layout;

namespace PDFiller.PDFManipulation
{
    public class PdfPlayground
    {
        public void FillUpFormAsFile(string source, string destination)
        {
            PdfReader pdfReader = new PdfReader(source);
            PdfWriter pdfWriter = new PdfWriter(destination);
            PdfDocument pdfDocument = new PdfDocument(pdfReader, pdfWriter);
            Document document = new Document(pdfDocument);
            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDocument, true);

            IDictionary<String, PdfFormField> fields = form.GetFormFields();

            PdfFormField toSet;
            fields.TryGetValue("JMB", out toSet);
            toSet.SetValue("123123123123112343");

            document.Close();
        }

        public MemoryStream FillUpFormAsMemoryStream(MemoryStream formToFill)
        {
            var writerStream = new MemoryStream();
            PdfReader pdfReader = new PdfReader(formToFill);
            PdfWriter pdfWriter = new PdfWriter(writerStream);
            PdfDocument pdfDocument = new PdfDocument(pdfReader, pdfWriter);
            Document document = new Document(pdfDocument);
            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDocument, true);

            IDictionary<String, PdfFormField> fields = form.GetFormFields();

            PdfFormField toSet;
            fields.TryGetValue("JMB", out toSet);
            toSet.SetValue("1234567890123");

            fields.TryGetValue("IME", out toSet);
            toSet.SetValue("Test");

            document.Close();

            return writerStream;
        }
    }
}
