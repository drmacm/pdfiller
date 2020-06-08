using System.Globalization;
using Diacritics;
using Diacritics.Extensions;

namespace PDFiller.Domain
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
            return RemoveSpaces(titleCaseInput).RemoveDiacritics();
        }

        private static string ReplaceSpaces(string input)
        {
            return input.Replace(" ", "-");
        }
        private static string RemoveSpaces(string input)
        {
            return input.Replace(" ", "");
        }
    }
}
