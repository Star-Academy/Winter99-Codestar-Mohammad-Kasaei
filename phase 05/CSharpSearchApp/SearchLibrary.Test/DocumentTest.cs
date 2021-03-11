using System;
using System.Collections.Generic;
using Xunit;
using System.Diagnostics.CodeAnalysis;


namespace SearchLibrary.Test
{
    [ExcludeFromCodeCoverage]
    public class DocumentTest
    {
        [Fact]
        public void DocumentNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Document(null));
        }

        [Fact]
        public void DocumentToStringTest()
        {
            var doc = new Document("testDocument");
            Assert.Equal("testDocument", doc.ToString());
        }
        
        
        [Fact]
        public void DocumentEqualNullTest()
        {
            var doc = new Document("testDocument");
            Assert.False(doc.Equals(null));
        }

        [Fact]
        public void DocumentEqualStringTest()
        {
            var doc = new Document("testDocument");
            Assert.False(doc.Equals(new String("mohammad")));
        }

        [Fact]
        public void DocumentEqualIdenticalTest()
        {
            var doc1 = new Document("testDocument");
            var doc2 = new Document("testDocument");
            Assert.Equal(doc1, doc2);
        }


        [Fact]
        public void DocumentHashCodeIdenticalTest()
        {
            var doc1 = new Document("testDocument");
            var doc2 = new Document("testDocument");
            Assert.Equal(doc1.GetHashCode(),doc2.GetHashCode());
        }

        [Fact]
        public void DocumentHashCodeDifferentTest()
        {
            var doc1 = new Document("testDocument1");
            var doc2 = new Document("testDocument2");
            Assert.NotEqual(doc1.GetHashCode(), doc2.GetHashCode());
        }

        [Fact]
        public void TokenizeDocumentTest()
        {
            var expected = new HashSet<Token>()
            {
                new Token("this"),
                new Token("is"),
                new Token("me"),
            };
            var tokens = Document.TokenizeContent("this is me??????||||||");
            Assert.Equal(expected, tokens);
        }
    }
}
