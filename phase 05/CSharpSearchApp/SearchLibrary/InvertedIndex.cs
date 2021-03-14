using System;
using System.Collections.Generic;

namespace SearchLibrary
{
    public class InvertedIndex
    {
        private readonly Dictionary<Token, DocumentSet> index;

        public InvertedIndex(Dictionary<Document, string> inputData)
        {
            this.index = new Dictionary<Token, DocumentSet>();
            AddDocuments(inputData);
        }
        public InvertedIndex():this(new Dictionary<Document, string>())
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
            foreach(var token in Tokenizer.TokenizeContent(content))
            {
                if (!index.ContainsKey(token))
                {
                    index.Add(token, new DocumentSet());
                }
                index[token].Add(document);
            }
        }

        public virtual DocumentSet SearchTokenInDocuments(Token token)
        {
            return index.ContainsKey(token) ? index[token] : new DocumentSet();
        }
    }
}
