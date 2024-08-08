using System.Net.Http;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FrontEnd.Services
{
  public class LearningUnitService : BaseService<LearningUnit>
  {
    public LearningUnitService(HttpClient client, ILogger<LearningUnitService> logger, IHttpContextAccessor httpContextAccessor)
        : base(client, logger, httpContextAccessor)
    {
    }
  }
}