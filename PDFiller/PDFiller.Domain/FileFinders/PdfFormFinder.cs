using System;
using System.IO;

namespace PDFiller.Domain.FileFinders
{
    public class PdfFormFinder : FileFinder
    {
        private static readonly string RelativePathToPdfForm = @"PDFiller\wwwroot\sample-data\src.pdf";

        public PdfFormFinder(string applicationFolder) : base(applicationFolder, RelativePathToPdfForm)
        {
        }
    }
}
