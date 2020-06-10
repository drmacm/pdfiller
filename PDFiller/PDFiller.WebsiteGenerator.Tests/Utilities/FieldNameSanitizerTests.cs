using PDFiller.Domain;
using Xunit;

namespace PDFiller.WebsiteGenerator.Tests.Utilities
{
    public class FieldNameSanitizerTests
    {
        [Fact]
        public void SanitizeForHtml_StringHasNoSpacesOrDiacritics_NoChanges()
        {
            var result = FieldNameSanitizer.SanitizeForHtml("niceName");

            Assert.Equal("niceName", result);
        }

        [Fact]
        public void SanitizeForHtml_StringHasSpaces_Sanitized()
        {
            var result = FieldNameSanitizer.SanitizeForHtml(" n i c e N a m e ");

            Assert.Equal("-n-i-c-e-N-a-m-e-", result);
        }

        [Fact]
        public void SanitizeForHtml_StringHasSpacesAndDiacritics_Sanitized()
        {
            var result = FieldNameSanitizer.SanitizeForHtml("Čč Ćć Žž Šš Đđ à É");

            Assert.Equal("Cc-Cc-Zz-Ss-Dd-a-E", result);
        }

        [Fact]
        public void SanitizeForCSharp_StringInPascalCase_NoChanges()
        {
            var result = FieldNameSanitizer.SanitizeForCSharp("Nicename");

            Assert.Equal("Nicename", result);
        }

        [Fact]
        public void SanitizeForCSharp_StringInCamelCase_Sanitized()
        {
            var result = FieldNameSanitizer.SanitizeForCSharp("niceName");

            Assert.Equal("Nicename", result);
        }

        [Fact]
        public void SanitizeForCSharp_StringInLowercaseWithSpaces_Sanitized()
        {
            var result = FieldNameSanitizer.SanitizeForCSharp("nice name");

            Assert.Equal("NiceName", result);
        }

        [Fact]
        public void SanitizeForCSharp_StringWithDots_Sanitized()
        {
            var result = FieldNameSanitizer.SanitizeForCSharp("nice.name");

            Assert.Equal("Nice_Name", result);
        }

        [Fact]
        public void SanitizeForCSharp_StringInLowercaseWithSpacesAndDiacritics_Sanitized()
        {
            var result = FieldNameSanitizer.SanitizeForCSharp("ččČ ććĆ đ à É");

            Assert.Equal("CccCccDAE", result);
        }
    }
}
