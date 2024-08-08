using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace APISDK
{
    public class APIClient
    {
        private readonly IHttpClientFactory _factory;
        private readonly Uri _baseAddress;

        public APIClient() { }

        public APIClient(IHttpClientFactory factory, string baseAPIAddress)
        {
            _factory = factory;
            _baseAddress = new Uri(baseAPIAddress);
        }

        public async Task<HttpResponseMessage> Get(string path, string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                path += $@"/{key}";
            }
            var client = _factory.CreateClient();
            client.BaseAddress = _baseAddress;
            var response = await client.GetAsync(path);
            return response;
        }

        public async Task<HttpResponseMessage> Post<T>(string path, T entity)
        {
            var client = _factory.CreateClient();
            client.BaseAddress = _baseAddress;
            var response = await client.PostAsJsonAsync(path, entity);
            return response;
        }

        public async Task<HttpResponseMessage> Put<T>(string path, string key, T entity)
        {
            if (!string.IsNullOrEmpty(key))
            {
                path += $@"/{key}";
            }

            var client = _factory.CreateClient();
            client.BaseAddress = _baseAddress;
            var response = await client.PutAsJsonAsync(path, entity);
            return response;
        }

        public async Task<HttpResponseMessage> Delete(string path, string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                path += $@"/{key}";
            }

            var client = _factory.CreateClient();
            client.BaseAddress = _baseAddress;
            var response = await client.DeleteAsync(path);
            return response;
        }
    }
}