using System;
using System.Collections.Generic;
using Nest;
using PeopleClientLibrary.validator;

namespace PeopleClientLibrary
{
    public abstract class ElasticObjectClient<T> where T : class
    {
        protected readonly ElasticClient Client;
        protected readonly string IndexName;

        protected ElasticObjectClient(string server, int port, string indexName, bool debugMode = false)
        {
            IndexName = indexName;
            var uri = new Uri($"{server}:{port}");
            var settings = new ConnectionSettings(uri);
            settings.DefaultIndex(IndexName);
            if (debugMode)
                settings.EnableDebugMode();
            Client = new ElasticClient(settings);
        }


        protected abstract CreateIndexDescriptor CreateMapping(CreateIndexDescriptor descriptor);

        public bool CheckConnection()
        {
            var response = Client.Ping().Validate();
            return response.ApiCall.Success;
        }

        public bool IndexDocument(T person)
        {
            var response = Client
                .IndexDocument(person)
                .Validate();
            return response.IsValid;
        }

        public bool Bulk(IEnumerable<T> items)
        {
            var bulkDescriptor = new BulkDescriptor();
            foreach (var item in items)
            {
                bulkDescriptor.Index<T>(d => d
                    .Index(IndexName)
                    .Document(item)
                );
            }

            var response = Client
                .Bulk(bulkDescriptor)
                .Validate();
            return response.IsValid;
        }

        public bool CreateIndex()
        {
            var response = Client
                .Indices
                .Create(IndexName, CreateMapping)
                .Validate();
            return response.Acknowledged;
        }

        public string CheckHealth()
        {
            var response = Client
                .Cluster
                .Health();
            return response.Status.ToString();
        }
    }
}