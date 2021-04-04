using System;
using System.Threading.Tasks;
using MyElasticLibrary;

namespace ConsoleApp
{
    class Program
    {
        private static readonly ITextInputOutput TextInputOutputObject = new TextInputOutput();
        private static readonly UserInterface.ICallback CallBackHandlerObject = new CallBackHandler();

        private static readonly UserInterface UserInterface =
            new TextUserInterface(CallBackHandlerObject, TextInputOutputObject);

        private static async Task Main()
        {
            await UserInterface.Start();
        }

        private class CallBackHandler : UserInterface.ICallback
        {
            private const string DefaultFileSettingsPath = @"../../../AppSettings.json";
            private AppSettings _settings;
            private MyElastic _elastic;

            public bool LoadAppSettings(string path)
            {
                try
                {
                    _settings = AppSettings.LoadFromFile(path);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }

            public bool InitMyElastic()
            {
                try
                {
                    _elastic = new MyElastic(_settings.Server, _settings.Port);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }

            public string GetDefaultAppSettingsFile()
            {
                return DefaultFileSettingsPath;
            }

            public async Task<bool> CheckConnectionAsync()
            {
                return await _elastic.IsAvailableAsync();
            }

            public async Task CreateIndex(string indexName)
            {
                await _elastic.CreateIndexAsync(indexName);
            }

            public async Task AddObjectToIndex(string indexName, string jsonString)
            {
                await _elastic.InsertData(indexName, jsonString);
            }

            public bool OnTerminate()
            {
                return true;
            }
        }
    }
}