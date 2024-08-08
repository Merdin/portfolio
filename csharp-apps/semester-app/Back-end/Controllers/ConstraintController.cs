using System.Collections.Generic;
using Backend.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Backend.Repositories;
using System;
using Microsoft.Extensions.Logging;
using Library.Models;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ConstraintController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly ILogger<ConstraintController> _logger;

        public ConstraintController(IRepositoryWrapper repoWrapper, ILogger<ConstraintController> logger)
        {
            _repoWrapper = repoWrapper;
            _logger = logger;
        }

        [HttpGet, Authorize(Roles = "ModulePrincipal")]
        public async Task<ActionResult<IEnumerable<Constraint>>> All()
        {
            try
            {
                return await _repoWrapper.Constraints.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred on All() within ConstraintController, exception message: '{ex.Message}'.");
                return StatusCode(500, "Something went wrong whilst updating the Constraint.");
            }
        }

        [HttpGet("{id}"), Authorize(Roles = "ModulePrincipal")]
        public async Task<ActionResult<Constraint>> Get(int id)
        {
            Constraint constraint = await _repoWrapper.Constraints.GetById(id);
            if (constraint == null) return NotFound();
            return constraint;
        }

        [HttpPost, Authorize(Roles = "ModulePrincipal")]
        public async Task<ActionResult<Constraint>> Create(Constraint constraint)
        {
            try
            {
                _repoWrapper.Constraints.Create(constraint);
                await _repoWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred on Create() within ConstraintController, exception message: '{ex.Message}'.");
                return StatusCode(500, "Something went wrong whilst creating the Constraint.");
            }
            return Ok();
        }

        [HttpPut("{id}"), Authorize(Roles = "ModulePrincipal")]
        public async Task<ActionResult<Constraint>> Update(int id, Constraint constraint)
        {
            if (id != constraint.Id) return BadRequest();
            if (await _repoWrapper.Constraints.GetById(id) == null) return NotFound();

            try
            {
                _repoWrapper.Constraints.Update(constraint);
                await _repoWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred on Update() within ConstraintController, exception message: '{ex.Message}'.");
                return StatusCode(500, "Something went wrong whilst updating the Constraint.");
            }
            return Ok();
        }

        [HttpDelete("{id}"), Authorize(Roles = "ModulePrincipal")]
        public async Task<ActionResult> Delete(int id)
        {
            Constraint constraint = await _repoWrapper.Constraints.GetById(id);
            if (constraint == null) return NotFound();

            try
            {
                _repoWrapper.Constraints.Delete(constraint);
                await _repoWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred on Delete() within ConstraintController, exception message: '{ex.Message}'.");
                return StatusCode(500, "Something went wrong whilst deleting the Module.");
            }
            return Ok();
        }
    }
}
