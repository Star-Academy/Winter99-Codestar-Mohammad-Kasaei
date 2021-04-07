using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    public class CommandLineUserInterface : UserInterface
    {
        public CommandLineUserInterface(IUserCallbacks callbacks) : base(callbacks)
        {
        }

        public override void Start()
        {
            Console.WriteLine("Welcome you are using people nest client");
            while (true)
            {
                var defaultAppSettingsPath = _callbacks.DefaultAppSettingsPath();
                var useDefaultAppSettingsPath =
                    AskYesNoQuestion($"Use Default AppSettings.json ({defaultAppSettingsPath}) ?");
                var path = useDefaultAppSettingsPath
                    ? defaultAppSettingsPath
                    : AskStringQuestion("Enter AppSettings.json path");
                if (_callbacks.Init(path))
                {
                    Console.WriteLine("App Settings loaded successfully");
                    break;
                }

                Console.WriteLine("Could not load settings file");
            }

            while (true)
            {
                var indexName = AskStringQuestion("Enter index name : ");
                var create = AskYesNoQuestion("Create the index (y) or already exists(n) ? ");
                if (_callbacks.IndexCreation(indexName, create))
                {
                    Console.WriteLine("Index created");
                    break;
                }

                Console.WriteLine("Could not create the index");
            }

            var isRunning = true;
            var menu = new Dictionary<string, Action>()
            {
                {
                    "bulk", () =>
                    {
                        var path = AskStringQuestion($"Enter file path relative to {Environment.CurrentDirectory} : ");
                        Console.WriteLine(_callbacks.BulkInsertFromFile(path)
                            ? "File loaded and inserted correctly"
                            : "Error happened with the file");
                    }
                },
                {
                    "exit", () =>
                    {
                        if (_callbacks.Terminate())
                        {
                            isRunning = false;
                        }
                        else
                        {
                            Console.WriteLine("Could not terminate the program due to operation");
                        }
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