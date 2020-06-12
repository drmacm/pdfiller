namespace PDFiller.WebsiteGenerator.FileManipulation.FileFinders
{
    public class PdfFormFinder : FileFinder
    {
        private static readonly string RelativePathToPdfForm = @"PDFiller.Website\wwwroot\src.pdf";

        public PdfFormFinder(string applicationFolder) : base(applicationFolder, RelativePathToPdfForm)
        {
        }
    }
}
