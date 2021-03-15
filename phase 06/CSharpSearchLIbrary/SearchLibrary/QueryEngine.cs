using System.Collections.Generic;
using System.Linq;

namespace SearchLibrary
{
    public class QueryEngine
    {
        private readonly InvertedIndex index;

        public QueryEngine(InvertedIndex index)
        {
            this.index = index;
        }

        public DocumentSet AdvancedSearch(params string[] inputValues)
        {
            var orWords = inputValues.Where(word => word.StartsWith("+"));
            var notWords = inputValues.Where(word => word.StartsWith("-"));
            var andWords = inputValues.Except(orWords).Except(notWords);

            var orTokens = orWords.Select(word => new Token(word[1..]));
            var andTokens = andWords.Select(word => new Token(word));
            var notTokens = notWords.Select(word => new Token(word[1..]));

            return AdvancedSearch(orTokens, andTokens, notTokens);
        }


        public DocumentSet AdvancedSearch(IEnumerable<Token> orTokens , IEnumerable<Token> andTokens, IEnumerable<Token> notTokens)
        {
            var orDocs = DocumentSet.Union(orTokens.Select(token => index.SearchTokenInDocuments(token)));
            var andDocs = DocumentSet.Intersection(andTokens.Select(token => index.SearchTokenInDocuments(token)));
            var notDocs = DocumentSet.Union(notTokens.Select(token => index.SearchTokenInDocuments(token)));

            DocumentSet result;
            if (orDocs.IsEmpty() || andDocs.IsEmpty())
            {
                result = DocumentSet.Union(orDocs, andDocs);
            }
            else
            {
                result = DocumentSet.Intersection(orDocs, andDocs);
            }
            result = DocumentSet.Subtract(result, notDocs);
            return result;
        }
    }
}
