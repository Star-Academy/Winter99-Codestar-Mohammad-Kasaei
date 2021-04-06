using System;
using System.IO;
using Newtonsoft.Json;

namespace ConsoleApp
{
    public class AppSettings
    {
        public AppSettings(int port, string server, string defaultSourceFilePath)
        {
            Port = port;
            Server = server;
            DefaultSourceFilePath = defaultSourceFilePath;
        }

        public string Server { get; }
        public int Port { get; }
        public string DefaultSourceFilePath { get; }

        public static AppSettings CreateFromFile(string filePath)
        {
            var content = File.ReadAllText(filePath);
            return CreateFromJsonString(content);
        }

        private static AppSettings CreateFromJsonString(string json)
        {
            var appSettings = JsonConvert.DeserializeObject<AppSettings>(json);
            if (appSettings is null)
                throw new ArgumentException("Could not convert json argument");
            return appSettings;
        }
    }
}