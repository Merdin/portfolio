using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.Repositories;
using Library.Models;
using Microsoft.Extensions.Logging;

namespace Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LearningUnitModuleController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly ILogger<LearningUnitModuleController> _logger;

        public LearningUnitModuleController(IRepositoryWrapper repoWrapper, ILogger<LearningUnitModuleController> logger)
        {
            _repoWrapper = repoWrapper;
            _logger = logger;
        }


        // POST: api/SemesterModule
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<LearningUnitModule>> Create(LearningUnitModule learningUnitModule)
        {
            try
            {
                _repoWrapper.LearningUnitModules.Create(learningUnitModule);
                await _repoWrapper.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred on Create() within LearningUnitModuleController, exception message: '{ex.Message}'.");
                return StatusCode(500, "Something went wrong whilst creating the ModuleConstraint.");
            }
        }

        [HttpDelete("{learningUnitId}")]
        public async Task<ActionResult<bool>> DeleteAllBySemesterId(int learningUnitId)
        {
            try
            {
                await _repoWrapper.LearningUnitModules.DeleteByLearningUnitId(learningUnitId);
                await _repoWrapper.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred on DeleteAllBySemesterId() within LearningUnitModuleController, exception message: '{ex.Message}'.");
                return StatusCode(500, "Something went wrong whilst creating the ModuleConstraint.");
            }
        }
    }
}
