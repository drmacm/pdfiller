using System;
using System.IO;

namespace PDFiller.Domain
{
    public class PdfFormFinder
    {
        private readonly string _applicationFolder;
       
        private readonly string _solutionFolder = "PDFiller";
        private readonly string _relativePathToPdfForm = @"PDFiller\wwwroot\sample-data\src.pdf";

        public PdfFormFinder(string applicationFolder)
        {
            if (string.IsNullOrEmpty(applicationFolder))
            {
                throw new ArgumentException("Expecting a path to the folder of the executable.");
            }

            _applicationFolder = applicationFolder;
        }

        public string GetPathToPdfForm()
        {
            var dir = new DirectoryInfo(_applicationFolder);

            //Expecting something like ...[SOLUTION]\[PROJECT]\bin\Debug\netcoreapp3.1\
            var solutionRoot = dir?.Parent?.Parent?.Parent?.Parent;
            if (solutionRoot == null || solutionRoot.Name != _solutionFolder)
            {
                throw new ArgumentException("Unexpected folder structure.");
            }

            return Path.Combine(solutionRoot.FullName, _relativePathToPdfForm);
        }
    }
}
