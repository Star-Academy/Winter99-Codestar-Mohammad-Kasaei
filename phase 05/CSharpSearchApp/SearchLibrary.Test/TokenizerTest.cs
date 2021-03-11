using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace SearchLibrary.Test
{
    [ExcludeFromCodeCoverage]
    public class TokenizerTest
    {
        [Fact]
        public void NormalizeTokenNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => Tokenizer.NormalizeToken(null));
        }

        [Fact]
        public void NormalizeTokenEmptyTest()
        {
            Assert.Equal("", Tokenizer.NormalizeToken(""));
        }

        [Fact]
        public void NormalizeTokenAllLowerCaseTest()
        {
            Assert.Equal("mohammad", Tokenizer.NormalizeToken("mohammad"));
        }

        [Fact]
        public void NormalizeTokenMixCaseTest()
        {
            Assert.Equal("mohammad", Tokenizer.NormalizeToken("Mohammad"));
        }

        [Fact]
        public void NormalizeTokenInvalidCharsTest()
        {
            Assert.Equal("mohammad", Tokenizer.NormalizeToken("Mohammad---??"));
        }

        [Fact]
        public void NormalizeTokenUnderLineTest()
        {
            Assert.Equal("mohammad", Tokenizer.NormalizeToken("Mohammad__"));
        }

        [Fact]
        public void NormalizeTokenMoreInvalidCharsTest()
        {
            Assert.Equal("mohammad", Tokenizer.NormalizeToken("---++??)(Mohammad__"));
        }
    }
}
