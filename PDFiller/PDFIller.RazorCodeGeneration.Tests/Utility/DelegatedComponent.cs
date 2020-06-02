using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace PDFiller.RazorCodeGeneration.Tests.Utility
{
    public class DelegatedComponent : ComponentBase
    {
        [Parameter] public RenderFragment Component { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder) => Component(builder);
    }
}