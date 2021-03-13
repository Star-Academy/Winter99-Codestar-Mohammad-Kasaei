using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace SearchLibrary.Test
{
    [ExcludeFromCodeCoverage]
    public class DocumentSetTest
    {
        /// <summary>
        /// just to make sure that the proper constructor is implemented.
        /// </summary>
        [Fact]
        public void DocumentSetCreationEmptyTest()
        {
            _ = new DocumentSet();
            Assert.True(true);
        }


        /// <summary>
        /// just to make sure that the proper constructor is implemented.
        /// </summary>
        [Fact]
        public void DocumentSetCreationInitTest()
        {
            var initializeSet = new HashSet<Document>() { new Document("doc1") , new Document("doc2")}; 
            _ = new DocumentSet(initializeSet);
            Assert.True(true);

        }

        [Fact]
        public void DocumentSetCreationNullHashSetTest()
        {
            Assert.Throws<ArgumentNullException>(() => _ = new DocumentSet((HashSet<Document>)null));
        }

        /// <summary>
        /// just to make sure that the proper constructor is implemented.
        /// </summary>
        [Fact]
        public void DocumentSetCreationEnumerableTest()
        {
            var initializeSet = new Document[] { new Document("doc1"), new Document("doc2") };
            _ = new DocumentSet(initializeSet);
            Assert.True(true);
        }

        [Fact]
        public void DocumentSetCreationParamsSingleTest()
        {
            var doc1 = new Document("doc1");
            var documentSet = new DocumentSet(doc1);
            Assert.True(documentSet.Has(doc1));
        }

        [Fact]
        public void DocumentSetCreationParamsMultiTest()
        {
            var doc1 = new Document("doc1");
            var doc2 = new Document("doc2");
            var documentSet = new DocumentSet(doc1,doc2);
            Assert.True(documentSet.Has(doc1) && documentSet.Has(doc2));
        }

        [Fact]
        public void DocumentSetCreationNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => new DocumentSet((IEnumerable<Document>)null));
        }

        [Fact]
        public void DocumentSetEqualNullTest()
        {
            var docSet = new DocumentSet();
            Assert.False(docSet.Equals(null));
        }

        [Fact]
        public void DocumentSetEqualIdenticalTest()
        {
            var doc = new DocumentSet();
            Assert.Equal(doc,doc);
        }

        [Fact]
        public void DocumentSetEqualTwoEmptyTest()
        {
            var docSet1 = new DocumentSet();
            var docSet2 = new DocumentSet();
            Assert.Equal(docSet1, docSet2);
        }

        [Fact]
        public void DocumentSetEqualStringTest()
        {
            var docSet1 = new DocumentSet();
            Assert.False(docSet1.Equals(new String("hello")));
        }

        [Fact]
        public void DocumentSetEqualDifferentSetsTest()
        {
            var docSet1 = new DocumentSet();
            var docSet2 = new DocumentSet(new HashSet<Document>{new Document("doc")});
            Assert.NotEqual(docSet1 , docSet2);
        }
        
        [Fact]
        public void DocumentSetEqualSameSetsTest()
        {
            var docSet1 = new DocumentSet(new HashSet<Document> { new Document("doc") });
            var docSet2 = new DocumentSet(new HashSet<Document> { new Document("doc") });
            Assert.Equal(docSet1 , docSet2);
        }


        [Fact]
        public void DocumentSetEqualDifferentSetsWithEqualItemsNumberTest()
        {
            var docSet1 = new DocumentSet(new HashSet<Document> { new Document("doc1") });
            var docSet2 = new DocumentSet(new HashSet<Document> { new Document("doc") });
            Assert.NotEqual(docSet1, docSet2);
        }


        [Fact]
        public void DocumentSetUnionWithNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => DocumentSet.Union(null));
        }

        [Fact]
        public void DocumentSetUnionOneSetTest()
        {
            var docSet1 = new DocumentSet(new HashSet<Document> { new Document("doc1") });
            Assert.Equal(docSet1, DocumentSet.Union(new DocumentSet[] { docSet1 }));
        }
        [Fact]
        public void DocumentSetUnionTwoIdenticalSetsTest()
        {
            var docSet1 = new DocumentSet(new HashSet<Document> { new Document("doc1") });
            Assert.Equal(docSet1, DocumentSet.Union(new DocumentSet[] { docSet1, docSet1 }));
        }    
        
        [Fact]
        public void DocumentSetUnionTwoSetsTest()
        {
            var docSet1 = new DocumentSet(new HashSet<Document> 
            { new Document("doc1") }
            );
            var docSet2 = new DocumentSet(new HashSet<Document> 
            { new Document("doc2") }
            );
            var expected = new DocumentSet(new HashSet<Document> 
            { new Document("doc1"), new Document("doc2") }
            );
            Assert.Equal(expected, DocumentSet.Union(new DocumentSet[] { docSet1, docSet2 }));
        }

        [Fact]
        public void DocumentSetSubtractTwoIdenticalTest()
        {
            var docSet = new DocumentSet(new HashSet<Document>
            { new Document("doc") });
            Assert.Equal(new DocumentSet(), DocumentSet.Subtract(docSet, docSet));
        }

        [Fact]
        public void DocumentSetSubtractSetFromEmptyTest()
        {
            var docSet = new DocumentSet(new HashSet<Document>
            { new Document("doc") });
            Assert.Equal(docSet, DocumentSet.Subtract(docSet, new DocumentSet()));
        }

        [Fact]
        public void DocumentSetSubtractTwoDifferentSetsTest()
        {
            var docSet1 = new DocumentSet(new HashSet<Document>
            { new Document("doc1") });
            var docSet2 = new DocumentSet(new HashSet<Document>
            { new Document("doc2") });
            Assert.Equal(docSet1, DocumentSet.Subtract(docSet1, docSet2));
        }

        [Fact]
        public void DocumentSetSubtractEmptyFromNonEmptyTest()
        {
            var docSet = new DocumentSet(new HashSet<Document>
            { new Document("doc") });
            Assert.Equal(new DocumentSet(), DocumentSet.Subtract(new DocumentSet(), docSet));
        }

        [Fact]
        public void DocumentSetHashCodeTwoIdenticalTest()
        {
            var docSet1 = new DocumentSet(new HashSet<Document>
            { new Document("doc") });
            var docSet2 = new DocumentSet(new HashSet<Document>
            { new Document("doc") });
            Assert.Equal(docSet1.GetHashCode() , docSet2.GetHashCode());
        }
        
        [Fact]
        public void DocumentSetIntersectIdenticalTest()
        {
            var docSet = new DocumentSet(new HashSet<Document>
            { new Document("doc") });
            Assert.Equal(docSet , DocumentSet.Intersection(docSet, docSet));
        }


        [Fact]
        public void DocumentSetIntersectEmptyAndFullTest()
        {
            var docSet = new DocumentSet(new HashSet<Document>
            { new Document("doc") });
            Assert.Equal(new(), DocumentSet.Intersection(new(), docSet));
        }

        [Fact]
        public void AddTest()
        {
            var docSet1 = new DocumentSet(new HashSet<Document>
            { new Document("doc") });
            docSet1.Add(new Document("doc2"));
            Assert.True(docSet1.Has(new Document("doc2")));
        }

        [Fact]
        public void HasTrueTest()
        {
            var docSet1 = new DocumentSet(new HashSet<Document>
            { new Document("doc") });
            Assert.True(docSet1.Has(new Document("doc")));
        }

        [Fact]
        public void HasFalseTest()
        {
            var docSet1 = new DocumentSet(new HashSet<Document>
            { new Document("doc") });
            Assert.False(docSet1.Has(new Document("doc2")));
        }

        [Fact]
        public void IsEmptyTrueTest()
        {
            var docSet1 = new DocumentSet();
            Assert.True(docSet1.IsEmpty());
        }

        [Fact]
        public void IsEmptyFalseTest()
        {
            var docSet1 = new DocumentSet(new HashSet<Document>
            { new Document("doc") });
            Assert.False(docSet1.IsEmpty());
        }

        [Fact]
        public void GetEnumerableTest()
        {
            var enumerable = new List<Document> { new Document("doc1"), new Document("doc2") } as IEnumerable<Document>;
            var docs = (new DocumentSet(enumerable)).GetEnumerable();
            Assert.True(enumerable.Count() == docs.Count() && (!enumerable.Except(docs).Any()));
        }
    }
}
