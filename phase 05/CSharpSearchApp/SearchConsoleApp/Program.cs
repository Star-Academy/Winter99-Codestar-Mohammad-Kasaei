using SearchLibrary;
using System;
using System.Collections.Generic;

namespace SearchConsoleApp
{
    class Program
    {
        private class CommandLineQueryEngine : CommandLine.ICommandLine
        {
            public DocumentSet SearchWordsList(string[] words)
            {
                return queryEngine.AdvancedSearch(words);
            }
        }
        private static readonly string dataPath = @"..\..\..\..\EnglishData";
        private static readonly Dictionary<Document, string> files = TextFileReader.ReadAllFilesInDirectory(dataPath);
        private static readonly InvertedIndex invertedIndex = new(files);
        private static readonly QueryEngine queryEngine = new(invertedIndex);
//        private static readonly /**/

        static void Main(string[] args)
        {
            CommandLine commandLine = new((words) => queryEngine.AdvancedSearch(words));
            commandLine.Start();
        }
    }
}
