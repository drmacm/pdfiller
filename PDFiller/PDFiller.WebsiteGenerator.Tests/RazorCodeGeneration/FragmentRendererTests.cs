﻿using System;
using Microsoft.AspNetCore.Components;
using PDFiller.WebsiteGenerator.RazorCodeGeneration;
using Xunit;

namespace PDFiller.WebsiteGenerator.Tests.RazorCodeGeneration
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

            RenderFragment paragraph = builder =>
            {
                builder.OpenElement(0, "p");
                builder.CloseElement();
            };

            var result = fragmentRenderer.Render(paragraph);

            Assert.Equal("<p></p>", result);
        }
    }
}