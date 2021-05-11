using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SearchWebApplication
{
    public class ApiQueryParser : IApiQueryParser
    {
        private const string SplitRegex = @"[,]+";
        private const string OrRegex = @"[+].+";
        private const string NotRegex = @"[-].+";
        private static readonly Func<string, string> WordsTrimmer = w => w.Trim('-').Trim('+');

        public void SplitWordsToGroups(string queryString,
            out string[] notWords,
            out string[] orWords,
            out string[] andWords
        )
        {
            var allWords = Regex.Split(queryString, SplitRegex);
            var orTerms = allWords.Where(w => Regex.IsMatch(w, OrRegex)).ToList();
            var notTerms = allWords.Where(w => Regex.IsMatch(w, NotRegex)).ToList();
            var andTerms = allWords.Except(orTerms).Except(notTerms).ToList();

            notWords = notTerms.Select(WordsTrimmer).ToArray();
            orWords = orTerms.Select(WordsTrimmer).ToArray();
            andWords = andTerms.ToArray();
        }
    }
}