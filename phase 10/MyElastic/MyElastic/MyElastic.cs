using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MyElasticLibrary
{
    public class MyElastic
    {
        private readonly HttpClient _client;

        public MyElastic(string serverUrl, int serverPort)
        {
            _client = new HttpClient
            {
                BaseAddress = new UriBuilder("http", serverUrl, serverPort).Uri
            };
        }

        public async Task<bool> IsAvailableAsync()
        {
            try
            {
                var httpResponseMessage = await _client.GetAsync("");
                return httpResponseMessage.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                // ignored
            }

            return false;
        }

        public async Task<bool> CreateIndexAsync(string indexName)
        {
            var httpResponseMessage = await _client.PutAsync(indexName, null);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                return false;
            }

            var responseString = await httpResponseMessage.Content.ReadAsStringAsync();
            var returnObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);
            return returnObject != null &&
                   returnObject.TryGetValue("acknowledged", out var acknowledged) &&
                   bool.TryParse(acknowledged, out var ackResult) &&
                   ackResult;
        }

        public async Task<bool> InsertData(string indexName, string jsonObject)
        {
            var httpResponseMessage = await _client.PostAsync($"{indexName}/_doc", new StringContent(jsonObject));
            return httpResponseMessage.IsSuccessStatusCode;
        }
    }
}