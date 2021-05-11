using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp
{
    public static class TextFileReader
    {
        public static List<Document> ReadAllFilesInDirectory(string folderPath)
        {
            return Directory
                .GetFiles(folderPath)
                .Select(ReadFileFromPath)
                .ToList();
        }

        public static Document ReadFileFromPath(string filePath)
        {
            return new Document(Path.GetFileName(filePath), File.ReadAllText(filePath));
        }
    }
}