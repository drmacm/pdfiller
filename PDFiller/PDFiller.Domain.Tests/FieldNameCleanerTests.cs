using System;
using System.IO;
using Xunit;

namespace PDFiller.Domain.Tests
{
    public class FieldNameCleanerTests
    {
        private readonly FieldNameCleaner _fieldNameCleaner;

        public FieldNameCleanerTests()
        {
            _fieldNameCleaner = new FieldNameCleaner();
        }

        [Fact]
        public void StringHasNoSpacesOrDiacritics_NoChanges()
        {
            var result = FieldNameCleaner.Clean("niceName");

            Assert.Equal("niceName", result);
        }

        [Fact]
        public void StringHasSpaces_CleanedCorrectly()
        {
            var result = FieldNameCleaner.Clean(" n i c e N a m e ");

            Assert.Equal("_n_i_c_e_N_a_m_e_", result);
        }

        [Fact]
        public void StringHasSpacesAndDiacritics_CleanedCorrectly()
        {
            var result = FieldNameCleaner.Clean("Čč Ćć Žž Šš Đđ à É");

            Assert.Equal("Cc_Cc_Zz_Ss_Dd_a_E", result);
        }
    }
}
