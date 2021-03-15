using Xunit;
using System;
using System.Diagnostics.CodeAnalysis;

namespace SearchLibrary.Test
{
    [ExcludeFromCodeCoverage]
    public class TokenTest
    {
        /// <summary>
        /// just to make sure that the proper constructor is implemented.
        /// </summary>
        [Fact]
        public void ConstructionTest()
        {
            _ = new Token("testString");
            Assert.True(true);
        } 
        
        [Fact]
        public void ConstructionEmptyTest()
        {
            Assert.Throws<ArgumentException>(() => { new Token(""); });
        }
        
        [Fact]
        public void ConstructionNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => { new Token(null); });
        }
        
        [Fact]
        public void ToStringTest()
        {
            var token = new Token("tESt____mE>>>>>>");
            Assert.Equal("testme", token.ToString());
        }

        [Fact]
        public void EqualsNullTest()
        {
            var token = new Token("tESt____mE>>>>>>");
            Assert.False(token.Equals(null));
        }

        [Fact]
        public void EqualsStringTest()
        {
            var token = new Token("tESt____mE>>>>>>");
            Assert.False(token.Equals("this is "));
        }

        [Fact]
        public void EqualsIdenticalTest()
        {
            var token = new Token("tESt____mE>>>>>>");
            Assert.True(token.Equals(token));
        }

        [Fact]
        public void EqualsTruePositiveTest()
        {
            var token1 = new Token("tESt____mE>>>>>>");
            var token2 = new Token("tESt____mE>>>>>><<<<");
            Assert.True(token1.Equals(token2));
        }

        [Fact]
        public void EqualsDifferentTest()
        {
            var token1 = new Token("tESt____mE>>>>>>");
            var token2 = new Token("FFtESt____mE>>>>>><<<<");
            Assert.False(token1.Equals(token2));
        }

        [Fact]
        public void HashCodeIdenticalTest()
        {
            var token1 = new Token("tESt____mE>>>>>>");
            Assert.Equal(token1.GetHashCode() , token1.GetHashCode());
        }

        [Fact]
        public void HashCodeEqualTest()
        {
            var token1 = new Token("tESt____mE>>>>>>");
            var token2 = new Token("tESt____mE>>>>>><<<<");
            Assert.Equal(token1.GetHashCode() , token2.GetHashCode());
        }

        [Fact]
        public void HashCodeDifferentTest()
        {
            var token1 = new Token("tESt____mE>>>>>>");
            var token2 = new Token("FFFtESt____mE>>>>>><<<<");
            Assert.NotEqual(token1.GetHashCode() , token2.GetHashCode());
        }
    }
}
