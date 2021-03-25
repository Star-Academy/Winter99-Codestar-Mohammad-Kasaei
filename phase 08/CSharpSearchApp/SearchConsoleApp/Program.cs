using Persistence;
using SearchLibrary;

namespace SearchConsoleApp
{
    class Program
    {
        private static readonly InvertedIndex InvertedIndex = new InvertedIndex(new Repository());
        private static readonly QueryEngine queryEngine = new QueryEngine(InvertedIndex);
        private static readonly CommandLine commandLine = new CommandLine(
                (words) => { return queryEngine.AdvancedSearch(words); },
                (path) =>
                {
                    var files = TextFileReader.ReadAllFilesInDirectory(path);
                    InvertedIndex.AddDocuments(files);
                    return true;
                }
                );

        static void Main(string[] args)
        {
            commandLine.Start();
        }
    }
}
