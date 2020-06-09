using System;
using System.IO;

namespace PDFiller.Domain.FileFinders
{
    public class FormModelFinder : FileFinder
    {
        private static readonly string RelativePathToHtmlForm = @"PDFiller\Models\FormModel.cs";

        public FormModelFinder(string applicationFolder) : base(applicationFolder, RelativePathToHtmlForm)
        {
        }
    }
}
