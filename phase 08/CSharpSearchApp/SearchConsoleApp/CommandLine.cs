using SearchLibrary;
using System;
using System.Linq;

namespace SearchConsoleApp
{
    public class CommandLine
    {

        private readonly Func<string[], DocumentSet> handle;
        private readonly Func<string , bool> dataLoader;

        public CommandLine(Func<string[] , DocumentSet> queryHandler , Func<string , bool> dataLoader)
        {
            this.handle = queryHandler;
            this.dataLoader = dataLoader;
        }

        public void Start()
        {
            PrintGreeting();
            string path = AskToInsertData();
            if(path!=null)
            {
                if (dataLoader(path))
                {
                    Console.WriteLine("Data loaded successfully.");
                }
                else
                {
                    Console.WriteLine("Could not load data");
                }
            }
            Console.WriteLine("Enter List of Queries:");
            var line = Console.ReadLine();
            var docs = handle(line.Split(" "));
            PrintResults(docs);           
        }

        private static void PrintGreeting()
        {
            Console.WriteLine("Welcome to Search engine\n\n");
        }

        private static string AskToInsertData()
        {
            Console.WriteLine("Insert new data to database ? (y/n)");
            var line = Console.ReadLine();
            if(line.ToLower() == "y")
            {
                Console.Write("Enter path : ");
                return Console.ReadLine();
            }
            return null;
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
