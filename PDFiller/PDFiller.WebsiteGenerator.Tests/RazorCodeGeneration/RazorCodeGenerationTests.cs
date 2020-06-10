using Bunit;
using Microsoft.AspNetCore.Components;
using PDFiller.WebsiteGenerator.RazorCodeGeneration;
using Xunit;

namespace PDFiller.WebsiteGenerator.Tests.RazorCodeGeneration
{
    public class RazorCodeGenerationTests
    {
        private readonly TestContext _testContext;

        public RazorCodeGenerationTests()
        {
            _testContext = new TestContext();
        }

        [Fact]
        public void GetHtmlFromBlazorComponent()
        {
            RenderFragment component = builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddContent(1, "Generated component");
                builder.CloseElement();
            };

            var cut = _testContext.RenderComponent<DelegatedComponent>((DelegatedComponent.ComponentPropertyName, component));
            var markup = cut.Markup;

            Assert.Equal("<div>Generated component</div>", markup);
        }
    }
}
