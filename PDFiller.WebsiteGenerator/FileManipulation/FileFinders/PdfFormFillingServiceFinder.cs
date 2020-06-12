namespace PDFiller.WebsiteGenerator.FileManipulation.FileFinders
{
    public class PdfFormFillingServiceFinder : FileFinder
    {
        private static readonly string RelativePathToPdfForm = @"PDFiller.Website\Services\PdfFormFillingService.cs";

        public PdfFormFillingServiceFinder(string applicationFolder) : base(applicationFolder, RelativePathToPdfForm)
        {
        }
    }
}
