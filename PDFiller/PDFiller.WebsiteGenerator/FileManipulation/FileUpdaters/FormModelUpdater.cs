using System;
using System.IO;

namespace PDFiller.WebsiteGenerator.FileManipulation.FileUpdaters
{
    public class FormModelUpdater
    {
        public void UpdateFormModel(string pathToFormModel, string sourceCode)
        {
            if (string.IsNullOrEmpty(pathToFormModel))
            {
                throw new ArgumentException("Path to FormModel class not provided.");
            }
            if (!File.Exists(pathToFormModel))
            {
                throw new ArgumentException("Invalid path to FormModel class.");
            }

            if (string.IsNullOrEmpty(sourceCode))
            {
                throw new ArgumentException("Source code is null or empty.");
            }

            File.WriteAllText(pathToFormModel, sourceCode);
        }
    }
}
