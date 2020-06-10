using PDFiller.Domain;

namespace PDFiller.WebsiteGenerator.RazorCodeGeneration.Fragments
{
    public static partial class FormFragments
    {
        public static string CheckBox(this FormField formField)
        {
            var id = @$"id=""{formField.HtmlFieldName}"" ";
               
            var cssClass = @"class=""form-check-input"" ";

            var bindValue = @$"@bind-Value=""formModel.{formField.CSharpFieldName}"" ";

            var inputType = @"type=""checkbox""";

          
            return @$"        <InputCheckbox {id}{cssClass}{bindValue}{inputType} />";
        }
    }
}
