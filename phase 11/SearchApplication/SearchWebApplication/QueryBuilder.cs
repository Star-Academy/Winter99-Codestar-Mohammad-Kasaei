using System.Collections.Generic;
using System.Linq;
using Nest;

namespace SearchWebApplication
{
    public class QueryBuilder : IQueryBuilder
    {
        public SearchRequest<Document> WordsToNestQueryObject(
            string index,
            IEnumerable<string> andWords,
            IEnumerable<string> orWords,
            IEnumerable<string> notWords
        )
        {
            var queryRequest = new SearchRequest<Document>(index)
            {
                Query = new BoolQuery
                {
                    Must = WordListToQueryContainerList(andWords),
                    Should = WordListToQueryContainerList(orWords),
                    MustNot = WordListToQueryContainerList(notWords)
                }
            };
            return queryRequest;
        }

        public IEnumerable<QueryContainer> WordListToQueryContainerList(IEnumerable<string> words)
        {
            return words.Select(word =>
                    new QueryContainer(
                        new MatchQuery
                        {
                            Field = Infer.Field<Document>(p => p.Content),
                            Query = word
                        }
                    )
                )
                .ToList();
        }
    }
}