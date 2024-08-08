using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using static Newtonsoft.Json.JsonConvert;

namespace FrontEnd.Services
{
    public abstract class BaseService<T> : IBaseService<T> where T : class
    {
        private HttpClient Client { get; }
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        protected BaseService(HttpClient client, ILogger logger, IHttpContextAccessor httpContextAccessor)
        {
            Client = client;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public async Task SetJwtToken()
        {
            var jwtToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            Client.SetBearerToken(jwtToken);
        }
        

        public StringContent Content(Object item)
        {
            return new StringContent(JsonSerializer.Serialize(item), Encoding.UTF8, "application/json");
        }

        public async Task<T> GetById(int id)
        {
            try
            {
                await SetJwtToken();
                var response = await Client.GetAsync($"{id}");

                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                return DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred on GetById() within {this.GetType().Name}, exception message: '{ex.Message}'.");
                return null;
            }
        }

        public async Task<List<T>> Index()
        {
            try
            {
                await SetJwtToken();
                var response = await Client.GetAsync("");

                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                return DeserializeObject<List<T>>(content);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred on Index() within {this.GetType().Name}, exception message: '{ex.Message}'.");
                return null;
            }
        }

        public async Task<T> Create(T item)
        {
            try
            {
                await SetJwtToken();
                var response = await Client.PostAsync($"", Content(item));
                
                response.EnsureSuccessStatusCode();
                
                string content = await response.Content.ReadAsStringAsync();
                return DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred on Create() within {this.GetType().Name}, exception message: '{ex.Message}'.");
                return null;
            }
        }

        public async Task<bool> Update(int id, T item)
        {
            try
            {
                await SetJwtToken();
                var response = await Client.PutAsync($"{id}", Content(item));
                
                response.EnsureSuccessStatusCode();
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred on Update() within {this.GetType().Name}, exception message: '{ex.Message}'.");
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                await SetJwtToken();
                var response = await Client.DeleteAsync($"{id}");
                
                response.EnsureSuccessStatusCode();
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception occurred on Delete() within {this.GetType().Name}, exception message: '{ex.Message}'.");
                return false;
            }
        }
        
    }
}