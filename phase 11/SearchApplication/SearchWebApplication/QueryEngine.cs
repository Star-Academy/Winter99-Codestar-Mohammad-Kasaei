using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Nest;
using SearchWebApplication.validator;

namespace SearchWebApplication
{
    public class QueryEngine : IQueryEngine
    {
        private readonly ElasticClient _client;

        public QueryEngine(IConfiguration configuration)
        {
            var host = configuration["host"];
            var port = configuration["port"];
            var uri = new Uri($"{host}:{port}");
            var settings = new ConnectionSettings(uri);
            _client = new ElasticClient(settings);
        }

        public IEnumerable<Document> GetDocsAdvancedQuery(SearchRequest<Document> searchRequest)
        {
            return _client
                .Search<Document>(searchRequest)
                .Validate()
                .Documents;
        }

        public IndexResponse Index(string index, Document document)
        {
            return _client
                .Index(document, i => i
                    .Index(index)
                )
                .Validate();
        }

        public bool CheckConnection()
        {
            try
            {
                _client.Ping().Validate();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}