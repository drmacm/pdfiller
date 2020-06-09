using System;
using System.IO;

namespace PDFiller.Domain.FileFinders
{
    public class FileFinder
    {
        private readonly string _applicationFolder;
        private readonly string _solutionFolder;
        private readonly string _relativePath;
        
        protected FileFinder(string applicationFolder, string relativePath)
        {
            _solutionFolder = "PDFiller";
            if (string.IsNullOrEmpty(applicationFolder))
            {
                throw new ArgumentException("Expecting a path to the folder of the executable.");
            }
            _applicationFolder = applicationFolder;
          
            if (string.IsNullOrEmpty(relativePath))
            {
                throw new ArgumentException("Expecting a relative path to the desired file.");
            }

            _relativePath = relativePath;
        }

        public string GetPath()
        {
            var dir = new DirectoryInfo(_applicationFolder);

            //Expecting something like ...[SOLUTION]\[PROJECT]\bin\Debug\netcoreapp3.1\
            var solutionRoot = dir?.Parent?.Parent?.Parent?.Parent;
            if (solutionRoot == null || solutionRoot.Name != _solutionFolder)
            {
                throw new ArgumentException("Unexpected folder structure.");
            }

            return Path.Combine(solutionRoot.FullName, _relativePath);
        }
    }
}
