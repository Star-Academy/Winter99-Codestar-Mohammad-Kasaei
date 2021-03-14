using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace SearchLibrary
{
    public class TextFileReader
    {
        public static Dictionary<Document , string> ReadAllFilesInDirectory(string folderPath)
        {
            var fullFilePaths = Directory.GetFiles(folderPath);
            var documents = fullFilePaths.Select(path => new Document(path[(folderPath.Length + 1)..]));
            var pairs = documents.Select(
                doc => new KeyValuePair<Document , string>(doc, File.ReadAllText(@$"{folderPath}/{doc}"))
            );
            return new Dictionary<Document, string>(pairs);
        }
    }
}
