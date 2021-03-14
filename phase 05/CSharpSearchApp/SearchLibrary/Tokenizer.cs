using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SearchLibrary
{
    public class Tokenizer
    {
        private static readonly string invalidRegex = @"[(\W+)(_)]";

        public static string NormalizeToken(String rawString)
        {
            if (rawString == null)
            {
                throw new ArgumentNullException(nameof(rawString));
            }

            return Regex.Replace(rawString.ToLower(), invalidRegex, "");
        }

        public static HashSet<Token> TokenizeContent(string content)
        {
            var tokenStrings = Regex.Split(content, @"\W+");
            var nonEmptyTokenStrings = tokenStrings.Where(stringValue => stringValue.Length > 0);
            var tokenItems = nonEmptyTokenStrings.Select(tokenString => new Token(tokenString));
            return new HashSet<Token>(tokenItems);
        }
    }
}
