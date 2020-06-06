using Bunit;
using Microsoft.AspNetCore.Components;
using PDFIller.RazorCodeGeneration.Tests.Utility;
using Xunit;

namespace PDFiller.RazorCodeGeneration.Tests
{
    public class RazorCodeGenerationTests
    {
        private readonly TestContext _testContext;

        private readonly string _parameterName = "Component";

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

            var cut = _testContext.RenderComponent<DelegatedComponent>((_parameterName, component));
            var markup = cut.Markup;

            Assert.Equal("<div>Generated component</div>", markup);
        }
    }
}
