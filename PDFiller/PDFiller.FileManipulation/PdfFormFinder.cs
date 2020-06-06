using System;
using System.IO;

namespace PDFiller.FileManipulation
{
    public class PdfFormFinder
    {
        private readonly string _solutionFolder = "PDFiller";
        private readonly string _relativePathToPdfForm = @"PDFiller\wwwroot\sample-data\src.pdf";
      
        public string PathToPdfForm { get; }

        public PdfFormFinder(string applicationFolder)
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

            PathToPdfForm = Path.Combine(solutionRoot.FullName, _relativePathToPdfForm);
        }
    }
}
