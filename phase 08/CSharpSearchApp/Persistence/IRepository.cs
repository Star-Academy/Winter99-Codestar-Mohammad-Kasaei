using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public interface IRepository
    {
        IEnumerable<string> GetDocumentsWithToken(string tokenString);
        void AddData(string documentName, string content, IEnumerable<string> tokens);
    }
}