using Moq;
using Persistence;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;


namespace SearchLibrary.Test
{
    [ExcludeFromCodeCoverage]
    public class InvertedIndexTest
    {
        private static readonly Document doc1 = new Document("doc1");
        private static readonly Document doc2 = new Document("doc2");
        private static readonly Dictionary<Document, string> sampleData = new Dictionary<Document, string>()
            {
                {doc1 , "this is content of the file one" },
                {doc2 , "this is content of the file two" }
            };
        private readonly InvertedIndex invertedIndex;
        private readonly IRepository repository;

        public InvertedIndexTest()
        {
            var mockedRepository = new Mock<IRepository>();
            mockedRepository
                .Setup((x => x.GetDocumentsWithToken("content")))
                .Returns(new List<string> { doc1.ToString(), doc2.ToString() });

            repository = mockedRepository.Object;
        }



        /// <summary>
        /// just to make sure that addDocument function works correctly and does not throw exception.
        /// </summary>
        [Fact]
        public void AddDocumentTest()
        {
            var invertedIndex = new InvertedIndex(repository);
            invertedIndex.AddDocument(new Document("doc"), "this is content in doc");
            Assert.True(true);
        }



        [Fact]
        public void QuerySimpleWithConsturctorDataTest()
        {
            var invertedIndex = new InvertedIndex(repository);
            var expected = new DocumentSet(new HashSet<Document>
                {doc1,doc2}
            );
            var actual = invertedIndex.SearchTokenInDocuments(new Token("content"));
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void QuerySimpleWithAddDocumentTest()
        {
            var invertedIndex = new InvertedIndex(repository);
            invertedIndex.AddDocument(doc1, "this is content of the file one");
            invertedIndex.AddDocument(doc2, "this is content of the file two");

            var expected = new DocumentSet(new HashSet<Document>
                {doc1,doc2}
            );
            var actual = invertedIndex.SearchTokenInDocuments(new Token("content"));
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void QuerySimpleWithAddDocumentsTest()
        {
            var invertedIndex = new InvertedIndex(repository);
            invertedIndex.AddDocuments(sampleData);
            var expected = new DocumentSet(new HashSet<Document>
                {doc1,doc2}
            );
            var actual = invertedIndex.SearchTokenInDocuments(new Token("content"));
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void QueryEmptyResultTest()
        {
            var invertedIndex = new InvertedIndex(repository);
            invertedIndex.AddDocuments(sampleData);
            var expected = new DocumentSet(new HashSet<Document>());
            var actual = invertedIndex.SearchTokenInDocuments(new Token("Mohammad"));
            Assert.Equal(expected, actual);
        }
    }
}
