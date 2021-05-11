using System.Collections.Generic;

namespace Persistence
{
    public interface IRepository
    {
        IEnumerable<string> GetDocumentsWithToken(string tokenString);
        void AddData(string documentName, string content, IEnumerable<string> tokens);
    }
}