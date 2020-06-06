using System;
using System.IO;

namespace PDFiller.FileManipulation
{
    public class PdfFormCopier
    {
        private readonly PdfFormFinder _pdfFormFinder;

        public string PathToPdfForm { get; }

        public PdfFormCopier(PdfFormFinder pdfFormFinder)
        {
            _pdfFormFinder = pdfFormFinder;
        }

        public void CopyPdfFormToBlazorProject(string pdfFormSource)
        {
            if (string.IsNullOrEmpty(pdfFormSource))
            {
                throw new ArgumentException("Path to source PDF form not provided.");
            }
            if (!File.Exists(pdfFormSource))
            {
                throw new ArgumentException("Invalid path to source PDF form.");
            }
            
            var pdfFormDestination = _pdfFormFinder.GetPathToPdfForm();

            File.Copy(pdfFormSource, pdfFormDestination);
        }
    }
}
