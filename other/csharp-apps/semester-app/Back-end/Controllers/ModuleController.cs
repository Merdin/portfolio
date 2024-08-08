using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Backend.Repositories;
using System;
using FluentEmail.Core;
using Microsoft.Extensions.Logging;
using Library.Models;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ModuleController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly ILogger<ModuleController> _logger;

        public ModuleController(IRepositoryWrapper repoWrapper, ILogger<ModuleController> logger)
        {
            _repoWrapper = repoWrapper;
            _logger = logger;
        }

        [HttpGet, Authorize(Roles = "Student")]
        public async Task<ActionResult<List<Module>>> All()
        {
            try
            {
                return await _repoWrapper.Modules.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception occurred on All() within ModuleController, exception message: '{ex.Message}'.");
                return StatusCode(500, "Something went wrong whilst creating the Module.");
            }
        }

        [HttpGet("Active"), Authorize(Roles = "Student")]
        public async Task<ActionResult<List<Module>>> GetAllActive()
        {
            try
            {
                return await _repoWrapper.Modules.GetAllActive();
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception occurred on All() within ModuleController, exception message: '{ex.Message}'.");
                return StatusCode(500, "Something went wrong whilst creating the Module.");
            }
        }

        [HttpGet("{id}"), Authorize(Roles = "Student")]
        public async Task<ActionResult<Module>> Get(int id)
        {
            Module module = await _repoWrapper.Modules.GetById(id);
            if (module == null) return NotFound();
            return module;
        }

        [HttpPost, Authorize(Roles = "ModulePrincipal")]
        public async Task<ActionResult<Module>> Create(Module module)
        {
            try
            {
                _repoWrapper.Modules.Create(module);
                await _repoWrapper.Save();
                return module;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception occurred on Create() within ModuleController, exception message: '{ex.Message}'.");
                return StatusCode(500, "Something went wrong whilst creating the Module.");
            }
        }

        [HttpPut("{id}"), Authorize(Roles = "ModulePrincipal")]
        public async Task<ActionResult<Module>> Update(int id, Module module, [FromServices] IFluentEmail mail)
        {
            if (id != module.Id) return BadRequest();
            if (await _repoWrapper.Modules.GetById(id) == null) return NotFound();

            try
            {
                _repoWrapper.Modules.Update(module);
                await _repoWrapper.Save();
                await module.SendUpdateMail(mail);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception occurred on Update() within ModuleController, exception message: '{ex.Message}'.");
                return StatusCode(500, $"Something went wrong whilst updating the Module.");
            }

            return Ok();
        }

        [HttpDelete("{id}"), Authorize(Roles = "ExamCommitteeMember")]
        public async Task<ActionResult> Delete(int id)
        {
            Module module = await _repoWrapper.Modules.GetById(id);
            if (module == null) return NotFound();

            try
            {
                _repoWrapper.Modules.Delete(module);
                await _repoWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception occurred on Delete() within ModuleController, exception message: '{ex.Message}'.");
                return StatusCode(500, "Something went wrong whilst deleting the Module.");
            }

            return Ok();
        }
    }
}