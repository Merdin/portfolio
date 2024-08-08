using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.Repositories;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ModuleConstraintController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly ILogger<ModuleController> _logger;

        public ModuleConstraintController(IRepositoryWrapper repoWrapper, ILogger<ModuleController> logger)
        {
            _repoWrapper = repoWrapper;
            _logger = logger;
        }
        
        [HttpPost, Authorize(Roles = "ModulePrincipal")]
        public async Task<ActionResult<ModuleConstraint>> Create(ModuleConstraint moduleConstraint)
        {
            try
            {
                _repoWrapper.ModuleConstraints.Create(moduleConstraint);
                await _repoWrapper.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred on Create() within ModuleConstraint, exception message: '{ex.Message}'.");
                return StatusCode(500, "Something went wrong whilst creating the ModuleConstraint.");
            }
        }

        [HttpDelete("{moduleId}"), Authorize(Roles = "ModulePrincipal")]
        public async Task<ActionResult<bool>> DeleteAllByModuleId(int moduleId)
        {
            try
            {
                await _repoWrapper.ModuleConstraints.DeleteByModuleId(moduleId);
                await _repoWrapper.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred on DeleteAllByModuleId() within ModuleConstraint, exception message: '{ex.Message}'.");
                return StatusCode(500, "Something went wrong whilst creating the ModuleConstraint.");
            }
        }
    }
}
