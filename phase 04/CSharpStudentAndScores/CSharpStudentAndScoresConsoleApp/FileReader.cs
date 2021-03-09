using System.IO;

namespace CSharpStudentAndScoresConsoleApp
{
    public class FileReader
    {
        string path;

        public FileReader(string path)
        {
            this.path = path;
        }

        public string readTextFile(string fileName)
        {
            return File.ReadAllText(path + fileName);
        }

    }
}
