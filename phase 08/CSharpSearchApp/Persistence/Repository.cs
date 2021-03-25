using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Persistence
{
    public class Repository
    {
        private readonly SearchContext searchContext = new SearchContext();

        public IEnumerable<string> GetDocumentsWithToken(string tokenString)
        {
            var token = searchContext.Tokens
                .Include(token => token.TokenDocumentModels)
                .Where(token => token.Value == tokenString)
                .SingleOrDefault();

            if (token != null && token.TokenDocumentModels != null && token.TokenDocumentModels.Count > 0)
            {
                return token.TokenDocumentModels
                    .Select(tokenDocumentModel => searchContext.Documents.Find(tokenDocumentModel.DocumentModelId).Name)
                    .ToList();
            }
            else
            {
                return new List<string>();
            }
        }

        public void AddData(string documentName, string content, IEnumerable<string> tokens)
        {
            var doc = searchContext.Documents.Where(doc => doc.Name == documentName).SingleOrDefault();
            if (doc == null)
            {
                doc = new DocumentModel() { Name = documentName, Content = content };
                searchContext.Add(doc);
            }

            foreach (var tokenStr in tokens)
            {
                var token = searchContext.Tokens.Where(token => token.Value == tokenStr).SingleOrDefault();
                if (token == null)
                {
                    token = new TokenModel()
                    {
                        Value = tokenStr
                    };
                    searchContext.Tokens.Add(token);
                }

                var tokenDocModel = searchContext.TokenDocuments.Find(token.Id, doc.Id);
                if (tokenDocModel == null)
                {
                    tokenDocModel = new TokenDocumentModel()
                    {
                        DocumentModel = doc,
                        DocumentModelId = doc.Id,
                        TokenModel = token,
                        TokenModelId = token.Id
                    };
                    searchContext.TokenDocuments.Add(tokenDocModel);
                }
            }
            searchContext.SaveChanges();
        }
    }
}
