using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace APISDK
{
    public class Service<T> : IService<T> where T : new()
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _factory;
        private readonly string _connectionString;

        public Service(IConfiguration configuration, IHttpClientFactory factory)
        {
            _configuration = configuration;
            _factory = factory;
            _connectionString = _configuration.GetValue<string>("BaseAPIAddress");
        }

        public async Task<bool> AddAsync(T item, string path)
        {
            var client = new APIClient(_factory, _connectionString);
            var response = await client.Post(path, item);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id, string path)
        {
            var client = new APIClient(_factory, _connectionString);
            var response = await client.Delete(path, id.ToString());
            return response.IsSuccessStatusCode;
        }

        public async Task<List<T>> GetAsync(string path)
        {
            var client = new APIClient(_factory, _connectionString);
            var response = await client.Get(path, string.Empty);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<T>>();
            }
            else
            {
                return new List<T>();
            }
        }

        public async Task<T> GetAsync(int id, string path)
        {
            var client = new APIClient(_factory, _connectionString);
            var response = await client.Get(path, id.ToString());
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task<bool> UpdateAsync(int id, T item, string path)
        {
            var client = new APIClient(_factory, _connectionString);
            var response = await client.Put(path, id.ToString(), item);
            return response.IsSuccessStatusCode;
        }
    }
}