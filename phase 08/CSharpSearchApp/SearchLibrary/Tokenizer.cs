using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SearchLibrary
{
    public class Tokenizer
    {
        private const string InvalidRegex = @"[(\W+)(_)]";
        private const string ContentTokenizationRegex = @"\W+";

        public static string NormalizeToken(string rawString)
        {
            if (rawString == null)
            {
                throw new ArgumentNullException(nameof(rawString));
            }

            return Regex.Replace(rawString.ToLower(), InvalidRegex, "");
        }

        public static HashSet<Token> TokenizeContent(string content)
        {
            var tokenStrings = Regex.Split(content, ContentTokenizationRegex);
            var nonEmptyTokenStrings = tokenStrings.Where(stringValue => stringValue.Length > 0);
            var tokenItems = nonEmptyTokenStrings.Select(tokenString => new Token(tokenString));
            return new HashSet<Token>(tokenItems);
        }
    }
}