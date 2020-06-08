using Microsoft.AspNetCore.Components;
using PDFiller.Domain;

namespace PDFiller.RazorCodeGeneration.Fragments
{
    public static partial class BlazorFragments
    {
        public static RenderFragment Label(this FormField formField)
        {
            return builder =>
            {
                builder.OpenElement(0, "label");
                builder.AddAttribute(0, "for", formField.HtmlFieldName);
                builder.AddContent(0, formField.FieldName);
                builder.CloseElement();
            };
        }
    }
}
