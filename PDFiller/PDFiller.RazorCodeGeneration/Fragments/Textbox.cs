using Microsoft.AspNetCore.Components;
using PDFiller.Domain;

namespace PDFiller.RazorCodeGeneration.Fragments
{
    public static partial class BlazorFragments
    {
        public static RenderFragment TextBox(this FormField formField)
        {
            return builder =>
            {
                builder.OpenElement(0, "input");
                builder.AddAttribute(0, "id", formField.FieldName);
                builder.AddAttribute(0, "name", formField.FieldName);
                builder.AddAttribute(0, "required", "required");
                builder.CloseElement();
            };
        }
    }
}
