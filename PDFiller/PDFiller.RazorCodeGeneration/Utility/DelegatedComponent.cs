using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace PDFiller.RazorCodeGeneration.Utility
{
    /// <summary>
    /// Component used to generate HTML markup from the <see cref="RenderFragment"/>,
    /// with the help of <see cref="Bunit"/> library and <see cref="TestContext"/>.
    /// The actual fragment to be rendered is passed from outside via the <see cref="Component"/> property.
    /// </summary>
    public class DelegatedComponent : ComponentBase
    {
        /// <summary>
        /// We need the name of the <see cref="Component"/> property to be able
        /// to pass the actual value of <see cref="RenderFragment"/> to be rendered.
        /// </summary>
        public static string ComponentPropertyName = nameof(Component);

        /// <summary>
        /// Represents the HTML fragment that will be rendered. Actual value is passed from outside.
        /// </summary>
        [Parameter] public RenderFragment Component { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder) => Component(builder);
    }
}