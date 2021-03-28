using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public interface IRepository
    {
        public IEnumerable<string> GetDocumentsWithToken(string tokenString);
        public void AddData(string documentName, string content, IEnumerable<string> tokens);
    }
}
