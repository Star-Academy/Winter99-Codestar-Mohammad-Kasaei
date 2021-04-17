using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    public class CommandLineUserInterface : UserInterface
    {
        public CommandLineUserInterface(IUserCallbacks callbacks) : base(callbacks)
        {
        }

        public override void Start()
        {
            Greeting();
            LoadAppSettings();
            SetupIndex();
            RunCommands();
        }

        private void RunCommands()
        {
            var isRunning = true;
            var menu = new Dictionary<string, Action>()
            {
                {
                    "bulk", () =>
                    {
                        var path = AskStringQuestion($"Enter file path relative to {Environment.CurrentDirectory} : ");
                        Console.WriteLine(Callbacks.BulkInsertFromFile(path)
                            ? "File loaded and inserted correctly"
                            : "Error happened with the file");
                    }
                },
                {
                    "exit", () =>
                    {
                        if (Callbacks.Terminate())
                        {
                            isRunning = false;
                        }
                        else
                        {
                            Console.WriteLine("Could not terminate the program due to operation");
                        }
                    }
                },
                {
                    "search", () =>
                    {
                        var response = Callbacks.SearchPeople(AskStringQuestion("Enter search args : ").Split(" "));
                        if (response is null)
                        {
                            Console.WriteLine("Response is null");
                        }
                        else if (response.IsEmpty)
                        {
                            Console.WriteLine("Response is empty");
                        }
                        else
                        {
                            Console.WriteLine(string.Join("\n", response.Select(p => p.Name)));
                        }
                    }
                },
                {
                    "report_age", () =>
                    {
                        var report = Callbacks.AgeReport();
                        var recordLines = report.Select(rec => $"{rec.Key}\t{rec.Value}");
                        var text = string.Join("\n", recordLines);
                        Console.Write(text);
                        Console.Write("\n\n");
                    }
                }
            };
            var menuString = "Actions : \n\t\t" + string.Join("\n\t\t", menu.Keys) + "\n";
            while (isRunning)
            {
                var cmd = AskStringQuestion(menuString);
                if (menu.TryGetValue(cmd, out var action))
                {
                    action.Invoke();
                }
                else
                {
                    Console.WriteLine("invalid Command");
                }
            }
        }

        private void SetupIndex()
        {
            while (true)
            {
                var indexName = AskStringQuestion("Enter index name : ");
                var create = AskYesNoQuestion("Create the index (y) or already exists(n) ? ");
                if (Callbacks.IndexCreation(indexName, create))
                {
                    Console.WriteLine("Index OK");
                    break;
                }

                Console.WriteLine("Could not create the index");
            }
        }

        private void LoadAppSettings()
        {
            while (true)
            {
                var defaultAppSettingsPath = Callbacks.DefaultAppSettingsPath();
                var useDefaultAppSettingsPath =
                    AskYesNoQuestion($"Use Default AppSettings.json ({defaultAppSettingsPath}) ?");
                var path = useDefaultAppSettingsPath
                    ? defaultAppSettingsPath
                    : AskStringQuestion("Enter AppSettings.json path");
                if (Callbacks.Init(path))
                {
                    Console.WriteLine("App Settings loaded successfully");
                    break;
                }

                Console.WriteLine("Could not load settings file");
            }
        }

        private static void Greeting()
        {
            Console.WriteLine("Welcome you are using people nest client");
        }

        private static bool AskYesNoQuestion(string question, string yesOption = "y", string noOption = "n")
        {
            while (true)
            {
                Console.Write($"{question} ({yesOption}/{noOption}) ");
                var input = Console.ReadLine();
                if (input == yesOption)
                {
                    return true;
                }

                if (input == noOption)
                {
                    return false;
                }
            }
        }

        private static string AskStringQuestion(string question, Func<string, bool> predicate)
        {
            while (true)
            {
                Console.Write($"{question} ");
                var input = Console.ReadLine();
                if (predicate(input))
                    return input;
            }
        }

        private static string AskStringQuestion(string question)
        {
            return AskStringQuestion(question, s => !string.IsNullOrEmpty(s));
        }
    }
}