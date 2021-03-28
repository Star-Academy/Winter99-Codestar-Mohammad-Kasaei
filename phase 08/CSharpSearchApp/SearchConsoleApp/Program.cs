using Persistence;
using SearchLibrary;
using System;

namespace SearchConsoleApp
{
    class Program
    {
        private static readonly InvertedIndex InvertedIndex = new InvertedIndex(new SQLServerRepo(".", "searchDB"));
        private static readonly QueryEngine queryEngine = new QueryEngine(InvertedIndex);
        private static readonly CommandLine commandLine = new CommandLine(
                (words) => { return queryEngine.AdvancedSearch(words); },
                (path) =>
                {
                    try
                    {
                        var files = TextFileReader.ReadAllFilesInDirectory(path);
                        InvertedIndex.AddDocuments(files);
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                );

        static void Main(string[] args)
        {
            commandLine.Start();
        }
    }
}
