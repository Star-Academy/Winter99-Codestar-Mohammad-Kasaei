using System.Collections.Generic;
using Nest;
using SearchWebApplication.validator;

namespace SearchWebApplication
{
    public class QueryEngine : IQueryEngine
    {
        private readonly ElasticClient _client;

        public QueryEngine(IHostConfigurationProvider configurationProvider)
        {
            var settings = new ConnectionSettings(configurationProvider.GetHostUrl());
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
    }
}