﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ConsoleApp.validator;
using Nest;

namespace ConsoleApp
{
    public class UserCallbacks : IUserCallbacks
    {
        private AppSettings _appSettings;
        private IElasticClient _client;

        public string DefaultAppSettingsPath()
        {
            return "../../../AppSettings.json";
        }

        public bool Init(string appSettingsPath)
        {
            try
            {
                _appSettings = AppSettings.CreateFromFile(appSettingsPath);
                if (_appSettings == null)
                    return false;

                var connectionSettings = new ConnectionSettings();
                connectionSettings.DefaultIndex(_appSettings.Index);
#if DEBUG
                connectionSettings.EnableDebugMode();
#endif
                _client = new ElasticClient(connectionSettings);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CheckServer()
        {
            return _client
                .Ping()
                .Validate()
                .IsValid;
        }

        public void InsertTextFile(string filePath)
        {
            _client
                .IndexDocument(
                    TextFileReader.ReadFileFromPath(filePath)
                )
                .Validate();
        }

        public void InsertFilesInFolder(string folderPath)
        {
            var descriptor = new BulkDescriptor();
            foreach (var doc in TextFileReader.ReadAllFilesInDirectory(folderPath))
            {
                descriptor
                    .Index<Document>(d => d
                        .Document(doc)
                    );
            }

            _client
                .Bulk(descriptor)
                .Validate();
        }

        public ImmutableList<Document> AdvancedQuery(
            IEnumerable<string> andWords,
            IEnumerable<string> orWords,
            IEnumerable<string> notWords
        )
        {
            var query = QueryBuilder.WordsToNestQueryObject(andWords, orWords, notWords);
            return _client
                .Search<Document>(query)
                .Validate()
                .Documents
                .ToImmutableList();
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