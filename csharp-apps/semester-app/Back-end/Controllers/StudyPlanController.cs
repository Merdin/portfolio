using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Backend.Repositories;
using Microsoft.Extensions.Logging;
using Library.Models;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class StudyPlanController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly ILogger<StudyPlanController> _logger;

        public StudyPlanController(IRepositoryWrapper repoWrapper, ILogger<StudyPlanController> logger)
        {
            _repoWrapper = repoWrapper;
            _logger = logger;
        }

        [HttpGet, Authorize(Roles = "StudyCareerCounselor")]
        public async Task<ActionResult<IEnumerable<StudyPlan>>> All()
        {
            try
            {
                return await _repoWrapper.StudyPlans.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred on All() with StudyPlanController, exception message: '{ex.Message}'.");
                return StatusCode(500, "Something went wrong whilst fetching all study-plans.");
            }
        }

        [HttpGet("user/{id}"), Authorize(Roles = "Student")]
        public async Task<ActionResult<List<StudyPlan>>> GetByUserId(int id)
        {
            try
            {
                return await _repoWrapper.StudyPlans.GetByUserId(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred on GetByUserId() with StudyPlanController, exception message: '{ex.Message}'.");
                return StatusCode(500, $"Something went wrong whilst fetching the study-plans owned by {id}.");
            }
        }
        
        [HttpGet("{id}"), Authorize(Roles = "Student")]
        public async Task<ActionResult<StudyPlan>> Get(int id)
        {
            StudyPlan studyPlan = await _repoWrapper.StudyPlans.GetById(id);
            if (studyPlan == null) return NotFound();
            return studyPlan;
        }

        [HttpPost, Authorize(Roles = "Student")]
        public async Task<ActionResult<StudyPlan>> Create(StudyPlan studyPlan)
        {
            try
            {
                _repoWrapper.StudyPlans.Create(studyPlan);
                await _repoWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred on Create() with StudyPlanController, exception message: '{ex.Message}'.");
                return StatusCode(500, "Something went wrong whilst creating the study-plan.");
            }
            return Ok();
        }

        [HttpPut("{id}"), Authorize(Roles = "Student")]
        public async Task<ActionResult<StudyPlan>> Update(int id, StudyPlan studyPlan)
        {
            if (id != studyPlan.Id) return BadRequest();
            if (await _repoWrapper.StudyPlans.GetById(id) == null) return NotFound();

            try
            {
                _repoWrapper.StudyPlans.Update(studyPlan);
                await _repoWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred on Update() within StudyPlanController, exception message: '{ex.Message}'.");
                return StatusCode(500, $"Something went wrong whilst updating the study-plan.");
            }
            return Ok();
        }

        [HttpDelete("{id}"), Authorize(Roles = "Student")]
        public async Task<ActionResult<StudyPlan>> Delete(int id)
        {
            StudyPlan studyPlan = await _repoWrapper.StudyPlans.GetById(id);
            if (studyPlan == null) return NotFound();

            try
            {
                _repoWrapper.StudyPlans.Delete(studyPlan);
                await _repoWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred on Delete() within StudyPlanController, exception message: '{ex.Message}'.");
                return StatusCode(500, "Something went wrong whilst deleting the study-plan.");   
            }
            return Ok();
        }
    }
}