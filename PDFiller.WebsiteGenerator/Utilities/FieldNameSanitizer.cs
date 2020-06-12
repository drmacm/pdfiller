using System.Globalization;
using Diacritics.Extensions;

namespace PDFiller.WebsiteGenerator.Utilities
{
    public static class FieldNameSanitizer
    {
        public static string SanitizeForHtml(string input)
        {
            return ReplaceSpaces(input).RemoveDiacritics();
        }

        public static string SanitizeForCSharp(string input)
        {
            var titleCaseInput = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input);
            return RemoveSpaces(titleCaseInput)
                .RemoveDots()
                .RemoveDiacritics();
        }

        private static string ReplaceSpaces(string input)
        {
            return input.Replace(" ", "-");
        }
        
        private static string RemoveSpaces(string input)
        {
            return input.Replace(" ", "");
        }
        
        private static string RemoveDots(this string input)
        {
            return input.Replace(".", "_");
        }
    }
}
