using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleApp
{
    public class CommandLineUserInterface : UserInterface
    {
        private const int MaxPrintContentLength = 100;

        public CommandLineUserInterface(IUserCallbacks callbacks) : base(callbacks)
        {
        }

        public override void Start()
        {
            Greeting();
            Initialization();

            Commands();
        }

        private void Commands()
        {
            var isRunning = true;
            var menu = new Dictionary<string, Action>()
            {
                {
                    "insert_folder", () =>
                    {
                        var path = AskStringQuestion(
                            $"Enter directory path relative to {Environment.CurrentDirectory} : ");
                        try
                        {
                            Callbacks.InsertFilesInFolder(path);
                            Console.WriteLine("Files in directory loaded and inserted correctly");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("Error happened action terminated");
                        }
                    }
                },
                {
                    "insert_document", () =>
                    {
                        var path = AskStringQuestion($"Enter file path relative to {Environment.CurrentDirectory} : ");
                        try
                        {
                            Callbacks.InsertTextFile(path);
                            Console.WriteLine("File loaded and inserted correctly");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("Error happened action terminated");
                        }
                    }
                },
                {
                    "search", () =>
                    {
                        var queryString = AskStringQuestion("Enter list of words (space separated) : ");
                        var allWords = Regex.Split(queryString, @"[ ]+");
                        var orTerms = allWords.Where(w => Regex.IsMatch(w, @"[+].+")).ToList();
                        var notTerms = allWords.Where(w => Regex.IsMatch(w, @"[-].+")).ToList();
                        var andTerms = allWords.Except(orTerms).Except(notTerms).ToList();

                        var notWords = notTerms.Select(w => w.Trim('-')).ToArray();
                        var orWords = orTerms.Select(w => w.Trim('+')).ToArray();
                        var andWords = andTerms.ToArray();

                        var response = Callbacks.AdvancedQuery(andWords, orWords, notWords);
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
                            Console.WriteLine(string.Join("\n",
                                response.Select(p => $@"Document {p.Id} ==>> {TruncateStringWithEllipse(p.Content)}")));
                        }
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

        private static string TruncateStringWithEllipse(string src, int maxLength = MaxPrintContentLength)
        {
            if (src.Length < maxLength)
                return src;

            return src[..maxLength] + "...";
        }

        private void Initialization()
        {
            var initDone = false;
            while (!initDone)
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
                    if (Callbacks.CheckServer())
                    {
                        Console.WriteLine("Connection to server is OK");
                        initDone = true;
                    }
                    else
                    {
                        Console.WriteLine("Could not connect to server");
                    }
                }
                else
                {
                    Console.WriteLine("Could not load settings file");
                }
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