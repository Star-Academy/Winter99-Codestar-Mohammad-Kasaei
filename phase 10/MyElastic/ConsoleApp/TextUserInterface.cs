using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class TextUserInterface : UserInterface
    {
        private readonly List<Command> _commands;

        private class Command
        {
            public string Shortcut { get; }
            public string Description { get; }
            public Action Operation { get; }

            public Command(string shortcut, string description, Action operation)
            {
                Shortcut = shortcut;
                Description = description;
                Operation = operation;
            }
        }

        private readonly ITextInputOutput _textInputOutput;
        private bool _isRunning = true;

        public TextUserInterface(ICallback callback, ITextInputOutput textInputOutput)
            : base(callback)
        {
            _textInputOutput = textInputOutput;
            _commands = new List<Command>
            {
                new Command("c", "Create index", () =>
                {
                    var indexName = AskStringQuestion("Enter index name : ");
                    Callback.CreateIndex(indexName);
                }),
                new Command("i", "Index document", () =>
                {
                    var indexName = AskStringQuestion("Enter target index name : ");
                    var objectJson = AskStringQuestion("Enter object json : ");
                    Callback.AddObjectToIndex(indexName, objectJson);
                }),
                new Command("e", "Exit", () =>
                {
                    if (Callback.OnTerminate())
                    {
                        _textInputOutput.Write("Closing program....\nGoodBye\n\n");
                        _isRunning = false;
                    }
                    else
                    {
                        _textInputOutput.Write("You can not close the program now ... ");
                    }
                })
            };
        }

        public override async Task Start()
        {
            _isRunning = true;
            if (!LoadAppSettingsPhase())
                return;
            if (!InitializationPhase())
                return;
            if (!await ConnectionCheckPhase())
                return;

            CommandsPhase();
        }

        private void CommandsPhase()
        {
            var items = string.Join(
                "\n",
                _commands.Select(
                    pair => $"\t{pair.Shortcut}\t\t{pair.Description}"
                )
            );
            var menu = $"Menu : \n{items}\n";

            while (_isRunning)
            {
                var userCommand = AskStringQuestion(menu);
                var command = _commands.SingleOrDefault(cmd => cmd.Shortcut == userCommand);
                if (command != null)
                {
                    command.Operation();
                }
                else
                {
                    _textInputOutput.Write("Invalid command.\n");
                }
            }
        }

        private async Task<bool> ConnectionCheckPhase()
        {
            if (await Callback.CheckConnectionAsync())
            {
                _textInputOutput.Write("Connection to elastic is OK\n");
                return true;
            }

            _textInputOutput.Write("Could not connect to elastic\n");
            return false;
        }

        private bool InitializationPhase()
        {
            if (Callback.InitMyElastic())
            {
                _textInputOutput.Write("Elastic Instance created successfully\n");
                return true;
            }

            _textInputOutput.Write("Could not create elastic instance\n");
            return false;
        }

        private bool LoadAppSettingsPhase()
        {
            var appSettingsDefaultPath = Callback.GetDefaultAppSettingsFile();
            while (true)
            {
                if (AskYesNoQuestion($"Change app settings path (default : {appSettingsDefaultPath}) ?"))
                {
                    appSettingsDefaultPath = AskStringQuestion("What is your app settings file path ?");
                }

                if (!Callback.LoadAppSettings(appSettingsDefaultPath))
                {
                    _textInputOutput.Write("Could not load app settings\n");
                }
                else
                {
                    _textInputOutput.Write("App settings loaded successfully\n");
                    return true;
                }
            }
        }

        private bool AskYesNoQuestion(string question, string positive = "y", string negative = "n")
        {
            do
            {
                _textInputOutput.Write($"{question}({positive}/{negative})");
                var input = _textInputOutput.Read().ToLower();
                if (input == positive)
                {
                    return true;
                }

                if (input == negative)
                {
                    return false;
                }
            } while (true);
        }

        private string AskStringQuestion(string question)
        {
            _textInputOutput.Write($"{question}");
            do
            {
                var input = _textInputOutput.Read();
                if (input.Length > 0)
                {
                    return input;
                }
            } while (true);
        }
    }
}