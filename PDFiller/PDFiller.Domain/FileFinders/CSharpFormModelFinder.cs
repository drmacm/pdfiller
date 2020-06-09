using System;
using System.IO;

namespace PDFiller.Domain.FileFinders
{
    public class CSharpFormModelFinder : FileFinder
    {
        private static readonly string RelativePathToHtmlForm = @"PDFiller\Models\FormModel.cs";

        public CSharpFormModelFinder(string applicationFolder) : base(applicationFolder, RelativePathToHtmlForm)
        {
        }
    }
}
