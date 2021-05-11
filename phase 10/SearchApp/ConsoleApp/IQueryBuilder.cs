using System.Collections.Generic;
using Nest;

namespace ConsoleApp
{
    public interface IQueryBuilder
    {
        SearchRequest<Document> WordsToNestQueryObject(
            IEnumerable<string> andWords,
            IEnumerable<string> orWords,
            IEnumerable<string> notWords
        );
    }
}