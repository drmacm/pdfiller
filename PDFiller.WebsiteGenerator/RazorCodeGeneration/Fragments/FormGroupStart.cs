namespace PDFiller.WebsiteGenerator.RazorCodeGeneration.Fragments
{
    public static partial class FormFragments
    {
        public static string FormGroupStart()
        {
            var cssClass = @"class=""form-group"" ";


            return $"    <div {cssClass}>";
        }
    }
}
