using SearchLibrary;
using System;
using System.Linq;

namespace SearchConsoleApp
{
    public class CommandLine
    {

        private readonly Func<string[], DocumentSet> handle;

        public interface ICommandLineCallback
        {
            public DocumentSet SearchWordsList(string[] words);
        }

        public CommandLine(Func<string[] , DocumentSet> handle)
        {
            this.handle = handle;
        }

        public void Start()
        {
            PrintGreeting();
            Console.WriteLine("Enter List of Queries:");
            var line = Console.ReadLine();
            var docs = handle(line.Split(" "));
            PrintResults(docs);           
        }

        private static void PrintGreeting()
        {
            Console.WriteLine("Welcome to Search engine\n\n");
        }

        private static void PrintResults(DocumentSet docs)
        {
            if (docs.GetEnumerable().Any())
            {
                foreach (var doc in docs.GetEnumerable())
                {
                    Console.WriteLine(@$"Document {doc}");
                }
            }
            else
            {
                Console.WriteLine(@$"No document found");
            }
        }
    }
}
