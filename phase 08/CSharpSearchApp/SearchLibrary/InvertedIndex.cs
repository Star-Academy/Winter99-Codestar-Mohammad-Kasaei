using Persistence;
using System.Linq;
using System.Collections.Generic;

namespace SearchLibrary
{
    public class InvertedIndex
    {
        private readonly IRepository repository;

        public InvertedIndex(IRepository repository)
        {
            this.repository = repository;
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
            AddData(document.ToString(), content);
        }

        private void AddData(string document, string content) {
            repository.AddData(document, content, Tokenizer.TokenizeContent(content).Select(token => token.ToString()));
        }

        public virtual DocumentSet SearchTokenInDocuments(Token token)
        {
            return new DocumentSet(
                        repository
                        .GetDocumentsWithToken(token.ToString())
                        .Select(str => new Document(str))
                    );
        }
    }
}
