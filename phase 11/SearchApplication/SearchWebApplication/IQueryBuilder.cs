using System.Collections.Generic;
using Nest;

namespace SearchWebApplication
{
    public interface IQueryBuilder
    {
        SearchRequest<Document> WordsToNestQueryObject(
            string indexName,
            IEnumerable<string> andWords,
            IEnumerable<string> orWords,
            IEnumerable<string> notWords
        );
    }
}