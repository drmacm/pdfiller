using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace PDFiller.RazorCodeGeneration
{
    public class HtmlFormFinder
    {
        private readonly string _solutionFolder = "PDFiller";
        private readonly string _relativePathToHtmlForm = @"PDFiller\Pages\HtmlForm.razor";
      
        public string PathToHtmlForm { get; }

        public HtmlFormFinder(string applicationFolder)
        {
            if (string.IsNullOrEmpty(applicationFolder))
            {
                throw new ArgumentException("Expecting a path to the folder of the executable.");
            }

            var dir = new DirectoryInfo(applicationFolder);

            //Expecting something like ...[SOLUTION]\[PROJECT]\bin\Debug\netcoreapp3.1\
            var solutionRoot = dir?.Parent?.Parent?.Parent?.Parent;
            if (solutionRoot == null || solutionRoot.Name != _solutionFolder)
            {
                throw new ArgumentException("Unexpected folder structure.");
            }

            PathToHtmlForm = Path.Join(solutionRoot.FullName, _relativePathToHtmlForm);
        }
    }
}
