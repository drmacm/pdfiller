using PDFiller.Models;

namespace PDFiller.WebsiteGenerator.RazorCodeGeneration.Fragments
{
    public static partial class FormFragments
    {
        public static string FieldHelp(this FormField formField)
        {
            var id = @$"id=""{formField.HtmlFieldName}Help"" ";
        
            var cssClass = @"class=""form-text text-muted"" ";

            var content = $"{formField.FieldName}";


            return @$"        <small {id}{cssClass}>{content}</small>";
        }
    }
}
