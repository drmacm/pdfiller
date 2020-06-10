using System;
using System.IO;

namespace PDFiller.Domain.FileUpdaters
{
    public class PdfFormFillingServiceUpdater
    {
        public void Update(string pathToService, string sourceCode)
        {
            if (string.IsNullOrEmpty(pathToService))
            {
                throw new ArgumentException("Path to Pdf form filling service class not provided.");
            }
            if (!File.Exists(pathToService))
            {
                throw new ArgumentException("Invalid path to Pdf form filling service class.");
            }

            if (string.IsNullOrEmpty(sourceCode))
            {
                throw new ArgumentException("Source code is null or empty.");
            }

            File.WriteAllText(pathToService, sourceCode);
        }
    }
}
