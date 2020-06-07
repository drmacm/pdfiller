using System;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace PDFiller.RazorCodeGeneration
{
    public interface IFragmentRenderer
    {
        string Render(RenderFragment renderFragment);
    }

    /// <summary>
    /// Generates HTML markup based on <see cref="RenderFragment"/> object.
    /// Relies on <see cref="Bunit"/>'s <see cref="TestContextBase.Renderer"/>.
    /// </summary>
    public class FragmentRenderer : IFragmentRenderer
    {
        private readonly TestContext _testContext;

        public FragmentRenderer()
        {
            _testContext = new TestContext();
        }

        public string Render(RenderFragment renderFragment)
        {
            if (renderFragment == null)
            {
                throw new ArgumentException("Fragment to render can't be null.");
            }

            var parameter = (DelegatedComponent.ComponentPropertyName, renderFragment);
            var renderedComponent = _testContext.RenderComponent<DelegatedComponent>(parameter);

            return renderedComponent.Markup;
        }
    }
}
