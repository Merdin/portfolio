using System.Net.Http;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
namespace FrontEnd.Services
{
  public class StudyPlanService : BaseService<StudyPlan>
  {
    public StudyPlanService(HttpClient client, ILogger<StudyPlanService> logger, IHttpContextAccessor httpContextAccessor, UserService userService)
        : base(client, logger, httpContextAccessor)
    {
    }
  }
}