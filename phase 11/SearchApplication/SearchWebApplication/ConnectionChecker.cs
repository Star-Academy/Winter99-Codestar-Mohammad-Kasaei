using System;
using Microsoft.Extensions.Configuration;
using Nest;
using SearchWebApplication.validator;

namespace SearchWebApplication
{
    public class ConnectionChecker : IConnectionChecker
    {
        private readonly ElasticClient _client;

        public ConnectionChecker(IConfiguration configuration)
        {
            var host = configuration["host"];
            var port = configuration["port"];
            var uri = new Uri($"{host}:{port}");
            var settings = new ConnectionSettings(uri);
            _client = new ElasticClient(settings);
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