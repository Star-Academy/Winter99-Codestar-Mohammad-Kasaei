using System;

namespace SearchLibrary
{
    public class Token
    {
        private readonly string tokenString;

        public Token(string tokenString)
        {
            if (tokenString == null)
            {
                throw new ArgumentNullException(nameof(tokenString));
            }else if ( tokenString.Length == 0)
            {
                throw new ArgumentException("Input string is empty");
            }
            this.tokenString = Tokenizer.NormalizeToken(tokenString);
        }

        public override bool Equals(object obj)
        {
            return obj is Token token && token.ToString().Equals(this.tokenString);
        }

        public override int GetHashCode()
        {
            return tokenString.GetHashCode();
        }

        public override string ToString()
        {
            return this.tokenString;
        }
    }
}
