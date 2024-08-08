using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using static Newtonsoft.Json.JsonConvert;

namespace FrontEnd.Services
{
    public class ModuleService : BaseService<Module>
    {
        private HttpClient Client { get; }
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ModuleService(HttpClient client, ILogger<ModuleService> logger, IHttpContextAccessor httpContextAccessor)
            : base(client, logger, httpContextAccessor)
        {
            Client = client;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Module>> GetActive()
        {
            try
            {
                await SetJwtToken();
                var response = await Client.GetAsync("Active");

                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                return DeserializeObject<List<Module>>(content);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception occurred on Index() within {this.GetType().Name}, exception message: '{ex.Message}'.");
                return null;
            }
        }
    }
}