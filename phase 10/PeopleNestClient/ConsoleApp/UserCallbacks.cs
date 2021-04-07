using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using PeopleClientLibrary;

namespace ConsoleApp
{
    public class UserCallbacks : IUserCallbacks
    {
        private AppSettings _appSettings;
        private PeopleClient _client;


        public string DefaultAppSettingsPath()
        {
            return "../../../AppSettings.json";
        }

        public bool Init(string appSettingsPath)
        {
            try
            {
                _appSettings = AppSettings.CreateFromFile(appSettingsPath);
                return _appSettings != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IndexCreation(string indexName, bool forceCreate)
        {
            try
            {
                _client = new PeopleClient(_appSettings.Server, _appSettings.Port, indexName, true);
                return forceCreate ? _client.CreateIndex() : _client.CheckConnection();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool BulkInsertFromFile(string filePath)
        {
            try
            {
                var content = File.ReadAllText(filePath);
                var people = JsonConvert.DeserializeObject<List<Person>>(content);
                return _client.Bulk(people);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Terminate()
        {
            /*
             * Nothing to do in terminate phase
             * User can always exit the program  :)
             */
            return true;
        }
    }
}