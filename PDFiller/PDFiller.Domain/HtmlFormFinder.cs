using System;
using System.IO;

namespace PDFiller.Domain
{
    public class HtmlFormFinder
    {
        private readonly string _applicationFolder;

        private readonly string _solutionFolder = "PDFiller";
        private readonly string _relativePathToHtmlForm = @"PDFiller\Pages\HtmlForm.razor";

        public HtmlFormFinder(string applicationFolder)
        {
            if (string.IsNullOrEmpty(applicationFolder))
            {
                throw new ArgumentException("Expecting a path to the folder of the executable.");
            }
            _applicationFolder = applicationFolder;
        }

        public string GetPathToHtmlForm()
        {
            var dir = new DirectoryInfo(_applicationFolder);

            //Expecting something like ...[SOLUTION]\[PROJECT]\bin\Debug\netcoreapp3.1\
            var solutionRoot = dir?.Parent?.Parent?.Parent?.Parent;
            if (solutionRoot == null || solutionRoot.Name != _solutionFolder)
            {
                throw new ArgumentException("Unexpected folder structure.");
            }

            return Path.Combine(solutionRoot.FullName, _relativePathToHtmlForm);
        }
    }
}
