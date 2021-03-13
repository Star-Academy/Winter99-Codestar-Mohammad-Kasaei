using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;


namespace SearchLibrary.Test
{
    [ExcludeFromCodeCoverage]
    public class InvertedIndexTest
    {
        /// <summary>
        /// just to make sure that the proper constructor is implemented.
        /// </summary>
        [Fact]
        public void ConstructorEmptyTest()
        {
            _ = new InvertedIndex();
            Assert.True(true);
        }


        /// <summary>
        /// just to make sure that the proper constructor is implemented.
        /// </summary>
        [Fact]
        public void ConstructorDictionaryTest()
        {
            var inputData = new Dictionary<Document, string>
            {
                {new Document("doc") , "this is content of the file" }
            };
            _ = new InvertedIndex(inputData);
            Assert.True(true);
        }


        /// <summary>
        /// just to make sure that addDocument function works correctly and does not throw exception.
        /// </summary>
        [Fact]
        public void AddDocumentTest()
        {
            var invertedIndex = new InvertedIndex();
            invertedIndex.AddDocument(new Document("doc"), "this is content in doc");
            Assert.True(true);
        }



        [Fact]
        public void QuerySimpleWithConsturctorDataTest()
        {
            var baseData = new Dictionary<Document, string>
            {
                {new Document("doc1") , "this is content of the file one" },
                {new Document("doc2") , "this is content of the file two" }
            };
            var invertedIndex = new InvertedIndex(baseData);

            var expected = new DocumentSet(new HashSet<Document>
            {
                new Document("doc1"),
                new Document("doc2")
            }
            );
            var actual = invertedIndex.SearchTokenInDocuments(new Token("content"));
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void QuerySimpleWithAddDocumentTest()
        {
            var invertedIndex = new InvertedIndex();
            invertedIndex.AddDocument(new Document("doc1"), "this is content of the file one");
            invertedIndex.AddDocument(new Document("doc2"), "this is content of the file two");

            var expected = new DocumentSet(new HashSet<Document>
            {
                new Document("doc1"),
                new Document("doc2")
            }
            );
            var actual = invertedIndex.SearchTokenInDocuments(new Token("content"));
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void QuerySimpleWithAddDocumentsTest()
        {
            var invertedIndex = new InvertedIndex();
            var baseData = new Dictionary<Document, string>
            {
                {new Document("doc1") , "this is content of the file one" },
                {new Document("doc2") , "this is content of the file two" }
            };
            invertedIndex.AddDocuments(baseData);
            var expected = new DocumentSet(new HashSet<Document>
            {
                new Document("doc1"),
                new Document("doc2")
            }
            );
            var actual = invertedIndex.SearchTokenInDocuments(new Token("content"));
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void QueryEmptyResultTest()
        {
            var invertedIndex = new InvertedIndex();
            var baseData = new Dictionary<Document, string>
            {
                {new Document("doc1") , "this is content of the file one" },
                {new Document("doc2") , "this is content of the file two" }
            };
            invertedIndex.AddDocuments(baseData);
            var expected = new DocumentSet(new HashSet<Document>());
            var actual = invertedIndex.SearchTokenInDocuments(new Token("Mohammad"));
            Assert.Equal(expected, actual);
        }
    }
}
