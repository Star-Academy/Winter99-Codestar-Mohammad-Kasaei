using System;
using Microsoft.Extensions.Configuration;
using Nest;
using SearchWebApplication.validator;

namespace SearchWebApplication
{
    public class ConnectionChecker : IConnectionChecker
    {
        private readonly ElasticClient _client;

        public ConnectionChecker(IHostConfigurationProvider configurationProvider)
        {
            var settings = new ConnectionSettings(configurationProvider.GetHostUrl());
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