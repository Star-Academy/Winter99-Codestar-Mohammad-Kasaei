using System;

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
            return id;
        }
    }
}
