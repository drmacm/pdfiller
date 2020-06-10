namespace PDFiller.WebsiteGenerator.FileManipulation.FileFinders
{
    public class HtmlFormFinder : FileFinder
    {
        private static readonly string RelativePathToHtmlForm = @"PDFiller\Pages\HtmlForm.razor";

        public HtmlFormFinder(string applicationFolder) : base(applicationFolder, RelativePathToHtmlForm)
        {
        }
    }
}
