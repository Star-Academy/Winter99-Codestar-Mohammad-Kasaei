using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SearchLibrary
{
    public class Document
    {
        private readonly string id;

        public Document(string id)
        {
            this.id = id ?? throw new ArgumentNullException(nameof(id));
        }

        public override bool Equals(object obj)
        {
            return obj is Document document &&
                   id == document.id;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        public override string ToString()
        {
            return this.id;
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
