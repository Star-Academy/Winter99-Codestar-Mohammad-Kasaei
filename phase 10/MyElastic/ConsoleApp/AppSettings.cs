using System;
using System.IO;
using Newtonsoft.Json;

namespace ConsoleApp
{
    public class AppSettings
    {
        public AppSettings(int port, string server)
        {
            Port = port;
            Server = server;
        }

        public string Server { get; }
        public int Port { get; }

        public static AppSettings LoadFromFile(string path)
        {
            var fileContent = File.ReadAllText(path);
            return LoadFromString(fileContent);
        }

        private static AppSettings LoadFromString(string jsonObjectString)
        {
            var settings = JsonConvert.DeserializeObject<AppSettings>(jsonObjectString);
            if (settings == null)
            {
                throw new ArgumentException("Could not parse the content to json");
            }

            return settings;
        }
    }
}