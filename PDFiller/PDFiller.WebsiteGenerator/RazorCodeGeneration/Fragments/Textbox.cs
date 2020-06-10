using PDFiller.Domain;

namespace PDFiller.WebsiteGenerator.RazorCodeGeneration.Fragments
{
    public static partial class FormFragments
    {
        public static string TextBox(this FormField formField)
        {
            var id = @$"id=""{formField.HtmlFieldName}"" ";
        
            var cssClass = @"class=""form-control"" ";
            
            var bindValue = @$"@bind-Value=""formModel.{formField.CSharpFieldName}"" ";
            
            var placeholder = @$"placeholder=""{formField.FieldName}"" ";
            
            var ariaDescribedBy = @$"aria-describedby=""{formField.HtmlFieldName}Help"" ";

            return $"        <InputText {id}{cssClass}{bindValue}{placeholder}{ariaDescribedBy} />";
        }
    }
}
