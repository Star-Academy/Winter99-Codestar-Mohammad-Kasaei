using System;
using System.Collections.Generic;
using System.IO;
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
    }
}
