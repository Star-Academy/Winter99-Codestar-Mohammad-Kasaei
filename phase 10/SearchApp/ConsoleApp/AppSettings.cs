﻿using System;
using System.IO;
using Newtonsoft.Json;

namespace ConsoleApp
{
    public class AppSettings
    {
        public AppSettings(int port, string server, string index)
        {
            Port = port;
            Server = server;
            Index = index;
        }

        public string Server { get; }
        public int Port { get; }
        public string Index { get; }

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