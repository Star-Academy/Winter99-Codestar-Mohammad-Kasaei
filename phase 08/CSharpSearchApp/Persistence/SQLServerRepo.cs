using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Persistence
{
    public class SqlServerRepo : IRepository
    {
        private readonly SearchContext searchContext;

        public SqlServerRepo(string serverPath, string dbName)
        {
            var optionsBuilder =
                new DbContextOptionsBuilder().UseSqlServer(
                    $"Server={serverPath};Database={dbName};Trusted_Connection=True;");
            searchContext = new SearchContext(optionsBuilder.Options);
        }

        public IEnumerable<string> GetDocumentsWithToken(string tokenString)
        {
            var token = searchContext.Tokens
                .Include(tokenObj => tokenObj.TokenDocumentModels)
                .SingleOrDefault(tokenObj => tokenObj.Value == tokenString);

            if (token != null && token.TokenDocumentModels.Count > 0)
            {
                return token.TokenDocumentModels
                    .Select(
                        tokenDocumentModel => searchContext.Documents.Find(tokenDocumentModel.DocumentModelId).Name
                    )
                    .ToList();
            }

            return new List<string>();
        }

        public void AddData(string documentName, string content, IEnumerable<string> tokens)
        {
            var doc = searchContext.Documents.SingleOrDefault(documentObj => documentObj.Name == documentName);
            if (doc == null)
            {
                doc = new DocumentModel {Name = documentName, Content = content};
                searchContext.Add(doc);
            }

            foreach (var tokenStr in tokens)
            {
                var token = searchContext.Tokens.SingleOrDefault(tokenObj => tokenObj.Value == tokenStr);
                if (token == null)
                {
                    token = new TokenModel
                    {
                        Value = tokenStr
                    };
                    searchContext.Tokens.Add(token);
                }

                var tokenDocModel = searchContext.TokenDocuments.Find(token.Id, doc.Id);
                if (tokenDocModel != null)
                    continue;

                tokenDocModel = new TokenDocumentModel
                {
                    DocumentModel = doc,
                    DocumentModelId = doc.Id,
                    TokenModel = token,
                    TokenModelId = token.Id
                };
                searchContext.TokenDocuments.Add(tokenDocModel);
            }

            searchContext.SaveChanges();
        }
    }
}