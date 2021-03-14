using SearchLibrary;
using System;
using System.Collections.Generic;

namespace SearchConsoleApp
{
    class Program
    {
        private static readonly string DataPath = @"..\..\..\..\EnglishData";
        private static readonly Dictionary<Document, string> Files = TextFileReader.ReadAllFilesInDirectory(DataPath);
        private static readonly InvertedIndex InvertedIndex = new(Files);
        private static readonly QueryEngine queryEngine = new(InvertedIndex);

        static void Main(string[] args)
        {
            CommandLine commandLine = new((words) => queryEngine.AdvancedSearch(words));
            commandLine.Start();
        }
    }
}
