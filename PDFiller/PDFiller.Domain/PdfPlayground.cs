using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
using iText.Layout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PDFiller.Domain
{
    public class PdfPlayground
    {
        public void Play()
        {
            PdfReader pdfReader = new PdfReader(@"C:\Users\Mladen\code\pdfiller\PDFiller\src.pdf");
            PdfWriter pdfWriter = new PdfWriter(@"C:\Users\Mladen\code\pdfiller\PDFiller\dest.pdf");
            PdfDocument pdfDocument = new PdfDocument(pdfReader, pdfWriter);
            Document document = new Document(pdfDocument);
            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDocument, true);

            IDictionary<String, PdfFormField> fields = form.GetFormFields();

            PdfFormField toSet;
            fields.TryGetValue("JMB", out toSet);
            toSet.SetValue("123123123123112343");

            document.Close();
        }

        public MemoryStream ExportToPdf(MemoryStream formToFill)
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
