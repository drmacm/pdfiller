namespace PDFiller.WebsiteGenerator.FileManipulation.FileFinders
{
    public class PdfFormFinder : FileFinder
    {
        private static readonly string RelativePathToPdfForm = @"PDFiller\wwwroot\sample-data\src.pdf";

        public PdfFormFinder(string applicationFolder) : base(applicationFolder, RelativePathToPdfForm)
        {
        }
    }
}
