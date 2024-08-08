using System.Net.Http;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FrontEnd.Services
{
    public class ConstraintService : BaseService<Constraint>
    {
        public ConstraintService(HttpClient client, ILogger<ConstraintService> logger, IHttpContextAccessor httpContextAccessor)
            : base(client, logger, httpContextAccessor)
        {
        }
    }
}