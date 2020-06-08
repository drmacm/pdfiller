using Diacritics;
using Diacritics.Extensions;

namespace PDFiller.Domain
{
    public class FieldNameCleaner
    {
        private static readonly DiacriticsMapper _diacriticsMapper = new DiacriticsMapper();

        
        public static string Clean(string input)
        {
            return RemoveSpaces(input).RemoveDiacritics();
        }

        private static string RemoveSpaces(string input)
        {
            return input.Replace(" ", "_");
        }
    }
}
