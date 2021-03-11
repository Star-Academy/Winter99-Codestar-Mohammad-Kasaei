using System;
using System.Collections.Generic;

namespace SearchLibrary
{
    public class InvertedIndex
    {
        private readonly Dictionary<Token, DocumentSet> index;

        public InvertedIndex(Dictionary<Document, string> inputData)
        {
            this.index = new();
            AddDocuments(inputData);
        }
        public InvertedIndex():this(new())
        {
        }
        public void AddDocuments(Dictionary<Document, string> documents)
        {
            foreach (var entry in documents)
            {
                AddDocument(entry.Key, entry.Value);
            }
        }

        public void AddDocument(Document document, string content)
        {
            foreach(var token in Document.TokenizeContent(content))
            {
                if (!index.ContainsKey(token))
                {
                    index.Add(token, new());
                }
                index[token].Add(document);
            }
        }

        public virtual DocumentSet SearchTokenInDocuments(Token token)
        {
            return index.ContainsKey(token) ? index[token] : new();
        }
    }
}
