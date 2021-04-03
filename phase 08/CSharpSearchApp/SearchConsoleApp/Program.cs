using Persistence;
using SearchLibrary;
using System;

namespace SearchConsoleApp
{
    internal static class Program
    {
        private static readonly InvertedIndex invertedIndex = new InvertedIndex(new SqlServerRepo(".", "searchDB"));
        private static readonly QueryEngine queryEngine = new QueryEngine(invertedIndex);

        private static readonly CommandLine commandLine = new CommandLine(
            words => queryEngine.AdvancedSearch(words),
            path =>
            {
                try
                {
                    var files = TextFileReader.ReadAllFilesInDirectory(path);
                    invertedIndex.AddDocuments(files);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        );

        private static void Main()
        {
            commandLine.Start();
        }
    }
}