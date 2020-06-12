using PDFiller.Models;

namespace PDFiller.WebsiteGenerator.RazorCodeGeneration.Fragments
{
    public static partial class FormFragments
    {
        public static string Label(this FormField formField)
        {
            var cssClass = formField.FieldType == FormFieldType.CheckBox ? @"class=""form-check-label"" " : "";
            var labelFor = $@"for=""{formField.HtmlFieldName}""";
            
            var content = $"{formField.FieldName}";


            return $"        <label {cssClass}{labelFor}>{content}</label>";
        }
    }
}
