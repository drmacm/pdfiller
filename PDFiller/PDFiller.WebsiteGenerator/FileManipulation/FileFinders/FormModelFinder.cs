namespace PDFiller.WebsiteGenerator.FileManipulation.FileFinders
{
    public class FormModelFinder : FileFinder
    {
        private static readonly string RelativePathToHtmlForm = @"PDFiller\Models\FormModel.cs";

        public FormModelFinder(string applicationFolder) : base(applicationFolder, RelativePathToHtmlForm)
        {
        }
    }
}
