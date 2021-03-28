using SearchLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchConsoleApp
{
    public class CommandLine
    {

        private readonly Func<string[], DocumentSet> handle;
        private readonly Func<string, bool> dataLoader;

        public CommandLine(Func<string[], DocumentSet> queryHandler, Func<string, bool> dataLoader)
        {
            this.handle = queryHandler;
            this.dataLoader = dataLoader;
        }

        public void Start()
        {
            PrintGreeting();
            AskYesNoQuestion("Index new documents ? ",
                () =>
                {
                    Console.WriteLine("Files directory : ");
                    var line = Console.ReadLine();
                    if (line.Length > 0 && dataLoader(line))
                    {
                        Console.WriteLine("Data loaded successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Could not load data");
                    }
                },
                () =>
                {
                });
            var words = InputNonEmptyLine("Enter List of Queries:");
            var docs = handle(words.Split(" "));
            PrintResults(docs);

        }

        private static void PrintGreeting()
        {
            Console.WriteLine("Index search engine ...... \n\n");
        }

        private static void AskYesNoQuestion(string question, Action yesAction, Action noAction)
        {
            var actions = new Dictionary<char, Action>
            {
                { 'y', yesAction },
                { 'n', noAction }
            };

            Console.WriteLine($"{question}(y/n) : ");
            while (true)
            {
                var inputLine = Console.ReadLine();
                if (inputLine.Length == 0)
                    continue;

                var ch = inputLine.ToLower().ToCharArray()[0];
                if (actions.ContainsKey(ch))
                {
                    actions[ch].Invoke();
                    return;
                }
            }
        }

        private static string InputNonEmptyLine(string text)
        {
            Console.WriteLine(text);
            while (true)
            {
                var line = Console.ReadLine();
                if (line.Length > 0)
                {
                    return line;
                }
            }
        }

        private static void PrintResults(DocumentSet docs)
        {
            Console.WriteLine(docs.GetEnumerable().Any() ?
                String.Join(" , ", docs.GetEnumerable()) : @$"No document found");
        }
    }
}
