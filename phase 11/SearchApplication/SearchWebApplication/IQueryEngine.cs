using System.Collections.Generic;
using Nest;

namespace SearchWebApplication
{
    public interface IQueryEngine
    {
        IEnumerable<Document> GetDocsAdvancedQuery(SearchRequest<Document> searchRequest);
        IndexResponse Index(string index, Document document);

        bool CheckConnection();
    }
}