using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Backend.Repositories;
using System;
using FluentEmail.Core;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Backend.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class LearningUnitController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly ILogger<LearningUnitController> _logger;

        public LearningUnitController(IRepositoryWrapper repoWrapper, ILogger<LearningUnitController> logger)
        {
            _repoWrapper = repoWrapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<LearningUnit>>> All()
        {
            try
            {
                var semesters = await _repoWrapper.LearningUnits.GetAll();
                return semesters;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred on All() within LearningUnitController, exception message: '{ex.Message}'.");
                return StatusCode(500, "Something went wrong whilst creating the LearningUnit.");
            }
        }
        
        [HttpPost, Authorize(Roles = "ModulePrincipal")]
        public async Task<ActionResult<LearningUnit>> Create(LearningUnit learningUnit)
        {
            try
            {
                _repoWrapper.LearningUnits.Create(learningUnit);
                await _repoWrapper.Save();
                return learningUnit;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred on Create() within LearningUnitController, exception message: '{ex.Message}'.");
                return StatusCode(500, "Something went wrong whilst creating the LearningUnit.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LearningUnit>> Get(int id)
        {
            LearningUnit semester = await _repoWrapper.LearningUnits.GetById(id);
            if (semester == null) return NotFound();
            return semester;
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult<LearningUnit>> Update(int id, LearningUnit semester, [FromServices] IFluentEmail mail)
        {
            if (id != semester.Id) return BadRequest();
            if (await _repoWrapper.LearningUnits.GetById(id) == null) return NotFound();

            try
            {
                _repoWrapper.LearningUnits.Update(semester);
                await _repoWrapper.Save();
                await semester.SendUpdateMail(mail);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred on Update() within LearningUnitController, exception message: '{ex.Message}'.");
                return StatusCode(500, $"Something went wrong whilst updating the LearningUnit.");
            }
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            LearningUnit learningUnit = await _repoWrapper.LearningUnits.GetById(id);
            if (learningUnit == null) return NotFound();

            try
            {
                _repoWrapper.LearningUnits.Delete(learningUnit);
                await _repoWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred on Delete() within ModuleController, exception message: '{ex.Message}'.");
                return StatusCode(500, "Something went wrong whilst deleting the LearningUnit.");
            }
            return Ok();
        }

    }
}
