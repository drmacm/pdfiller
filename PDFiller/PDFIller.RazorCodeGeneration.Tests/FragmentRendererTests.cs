using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Xunit;

namespace PDFiller.RazorCodeGeneration.Tests
{
    public class FragmentRendererTests
    {
        [Fact]
        public void RenderFragmentNull_ShouldThrow()
        {
            var fragmentRenderer = new FragmentRenderer();

            Action action = () => fragmentRenderer.Render(null);

            var exception = Assert.Throws<ArgumentException>(action);

            Assert.Equal("Fragment to render can't be null.", exception.Message);
        }

        [Fact]
        public void ParagraphRenderFragment_ShouldReturnHTML()
        {
            var fragmentRenderer = new FragmentRenderer();
            
            static void Paragraph(RenderTreeBuilder builder)
            {
                builder.OpenElement(0, "p");
                builder.CloseElement();
            }

            var result = fragmentRenderer.Render(Paragraph);

            Assert.Equal("<p></p>", result);
        }
    }
}
