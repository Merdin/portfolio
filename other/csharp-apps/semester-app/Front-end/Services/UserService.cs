using System;
using System.Net.Http;
using System.Threading.Tasks;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using static Newtonsoft.Json.JsonConvert;

namespace FrontEnd.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private new HttpClient Client { get; }
        private new readonly ILogger<UserService> _logger;

        public UserService(HttpClient client, ILogger<UserService> logger, IHttpContextAccessor httpContextAccessor)
            : base(client, logger, httpContextAccessor)
        {
            Client = client;
            _logger = logger;
        }

        public async Task<bool> IsCurrentUserRegistered()
        {
            try
            {
                return await GetCurrentUser() != null;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception occurred on IsCurrentUserRegistered() within UserService, exception message: '{ex.Message}'.");
                return false;
            }
        }

        public async Task<User> GetCurrentUser()
        {
            try
            {
                await SetJwtToken();
                var response = await Client.GetAsync("current");

                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                return DeserializeObject<User>(content);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception occurred on GetCurrentUser() within UserService, exception message: '{ex.Message}'.");
                return null;
            }
        }

        public async Task<bool> EditUser(int id, User item)
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
                _logger.LogError(
                    $"Exception occurred on EditUser() within UserService, exception message: '{ex.Message}'.");
                return false;
            }
        }
    }
}