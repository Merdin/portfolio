using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.Repositories;
using Microsoft.Extensions.Logging;
using Library.Models;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IRepositoryWrapper repoWrapper, ILogger<UserController> logger)
        {
            _repoWrapper = repoWrapper;
            _logger = logger;
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<User>>> All()
        {
            try
            {
                return await _repoWrapper.Users.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception occurred on All() within UserController, exception message: '{ex.Message}'.");
                return StatusCode(500, "Something went wrong whilst fetching all the Users.");
            }
        }

        [HttpGet("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<User>> Get(int id)
        {
            User user = await _repoWrapper.Users.GetById(id);
            if (user == null) return NotFound();
            return user;
        }

        [HttpPut("{id}"), Authorize(Roles = "Student")]
        public async Task<IActionResult> Update(int id, User user)
        {
            if (id != user.Id) return BadRequest();
            if (await _repoWrapper.Users.GetById(id) == null) return NotFound();

            try
            {
                _repoWrapper.Users.Update(user);
                await _repoWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception occurred on Update() within UserController, exception message: '{ex.Message}'.");
                return StatusCode(500, $"Something went wrong whilst updating the Module.");
            }

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            try
            {
                _repoWrapper.Users.Create(user);
                await _repoWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception occurred on Create() within UserController, exception message: '{ex.Message}'.");
                return StatusCode(500, "Something went wrong whilst creating the User.");
            }

            return Ok();
        }

        [HttpPost("exists")]
        public async Task<ActionResult<bool>> Exists(User user)
        {
            try
            {
                return await _repoWrapper.Users.UserExists(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception occurred on Exists() within UserController, exception message: '{ex.Message}'.");
                return StatusCode(500, "Something went wrong whilst checking if the user exists.");
            }
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<User>> Delete(int id)
        {
            User user = await _repoWrapper.Users.GetById(id);
            if (user == null) return NotFound();

            try
            {
                _repoWrapper.Users.Delete(user);
                await _repoWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Exception occurred on Delete() within UserController, exception message: '{ex.Message}'.");
                return StatusCode(500, "Something went wrong whilst deleting the User.");
            }

            return Ok();
        }

        [HttpGet("current"), Authorize]
        public async Task<ActionResult<User>> GetCurrentUser()
        {
            var claimedUser = Library.Models.User.Create(HttpContext.User.Identity as ClaimsIdentity);
            return await _repoWrapper.Users.GetByToken(claimedUser.Token);
        }
    }
}