using System.Net.Http;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FrontEnd.Services
{
  public class LearningUnitModuleService : BaseService<LearningUnitModule>
  {
    public LearningUnitModuleService(HttpClient client, ILogger<LearningUnitModuleService> logger, IHttpContextAccessor httpContextAccessor)
        : base(client, logger, httpContextAccessor)
    {
    }
  }
}