using System.Collections.Generic;
using System.Linq;
using Nest;

namespace ConsoleApp
{
    public static class QueryBuilder
    {
        public static SearchRequest<Document> WordsToNestQueryObject(
            IEnumerable<string> andWords,
            IEnumerable<string> orWords,
            IEnumerable<string> notWords
        )
        {
            var queryRequest = new SearchRequest<Document>
            {
                Query = new BoolQuery
                {
                    Must = WordListToQuery(andWords),
                    Should = WordListToQuery(orWords),
                    MustNot = WordListToQuery(notWords)
                }
            };
            return queryRequest;
        }

        private static IEnumerable<QueryContainer> WordListToQuery(IEnumerable<string> words)
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