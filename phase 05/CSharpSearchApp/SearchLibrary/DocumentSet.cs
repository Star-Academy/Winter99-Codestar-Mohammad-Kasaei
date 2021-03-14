using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchLibrary
{
    public class DocumentSet
    {
        private readonly HashSet<Document> documents;

        public DocumentSet():this(Array.Empty<Document>())
        {
        }

        public DocumentSet(params Document[] documents):this(documents.AsEnumerable())
        {
        }

        public DocumentSet(HashSet<Document> initializeSet)
        {
            this.documents = initializeSet ?? throw new ArgumentNullException(nameof(initializeSet));
        }
        public DocumentSet(IEnumerable<Document> initializer):this(new HashSet<Document>(initializer))
        {
        }

        public override bool Equals(object obj)
        {
            return obj is DocumentSet documentSet && documentSet.documents.SetEquals(documents);
        }

        public static DocumentSet Union(params DocumentSet[] documentSets)
        {
            return Union(documentSets.AsEnumerable());
        }

        public static DocumentSet Union(IEnumerable<DocumentSet> documentSets)
        {
            if (documentSets == null)
                throw new ArgumentNullException(nameof(documentSets));
            var docs = new HashSet<Document>();
            foreach(var set in documentSets)
            {
                docs.UnionWith(set.documents);
            }
            return new DocumentSet(docs);
        }

        public static DocumentSet Subtract(DocumentSet baseDocSet , DocumentSet excluding)
        {
            return new DocumentSet(baseDocSet.documents.Except(excluding.documents));
        }

        public static DocumentSet Intersection(params DocumentSet[] documentSets)
        {
            return Intersection(documentSets.AsEnumerable());
        }

        public static DocumentSet Intersection(IEnumerable<DocumentSet> documentSets)
        {
            HashSet<Document> result = null;
            foreach(var documents in documentSets.Select(docSet=> docSet.documents))
            {
                if (result == null)
                {
                    result = new HashSet<Document>(documents);
                }
                else
                {
                    result.IntersectWith(documents);
                }
            }
            return result == null ? new DocumentSet() : new DocumentSet(result);
        }

        public override int GetHashCode()
        {
            return documents.Count;
        }

        public bool Has(Document document)
        {
            return this.documents.Contains(document);
        }

        public void Add(Document document)
        {
            this.documents.Add(document);
        }
        
        public bool IsEmpty()
        {
            return documents.Count == 0;
        }

        public IEnumerable<Document> GetEnumerable()
        {
            return documents.AsEnumerable();
        }
    }
}
