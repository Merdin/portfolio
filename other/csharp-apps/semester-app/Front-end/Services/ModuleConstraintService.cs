using System.Net.Http;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FrontEnd.Services
{
    public class ModuleConstraintService : BaseService<ModuleConstraint>
    {
        public ModuleConstraintService(HttpClient client, ILogger<ModuleConstraintService> logger, IHttpContextAccessor httpContextAccessor)
            : base(client, logger, httpContextAccessor)
        {
        }
    }
}