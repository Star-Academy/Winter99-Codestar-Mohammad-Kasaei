using System;
using Microsoft.Extensions.Configuration;

namespace SearchWebApplication
{
    public class HostConfigurationProvider : IHostConfigurationProvider
    {
        private readonly IConfiguration _configuration;

        public HostConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Uri GetHostUrl()
        {
            var host = _configuration["host"];
            var port = _configuration["port"];
            return new Uri($"{host}:{port}");
        }
    }
}