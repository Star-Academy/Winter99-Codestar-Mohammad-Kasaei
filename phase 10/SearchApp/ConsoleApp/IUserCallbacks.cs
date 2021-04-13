using System.Collections.Generic;
using System.Collections.Immutable;

namespace ConsoleApp
{
    public interface IUserCallbacks
    {
        string DefaultAppSettingsPath();
        bool Init(string appSettingsPath);

        bool CheckServer();
        void InsertTextFile(string filePath);
        void InsertFilesInFolder(string folderPath);

        ImmutableList<Document> AdvancedQuery(
            IEnumerable<string> andWords,
            IEnumerable<string> orWords,
            IEnumerable<string> notWords
        );

        bool Terminate();
    }
}