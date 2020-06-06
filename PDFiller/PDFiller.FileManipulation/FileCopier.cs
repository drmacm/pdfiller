using System;
using System.IO;

namespace PDFiller.FileManipulation
{
    public class FileCopier
    {
        public static void CopyPdfFormToBlazorProject(string pdfFormSource, string pdfFormDestination)
        {
            if (string.IsNullOrEmpty(pdfFormSource))
            {
                throw new ArgumentException("Path to source PDF form not provided.");
            }
            if (!File.Exists(pdfFormSource))
            {
                throw new ArgumentException("Invalid path to source PDF form.");
            }
            
            File.Copy(pdfFormSource, pdfFormDestination, true);
        }
    }
}
