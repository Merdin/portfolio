using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FrontEnd.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Library.Models;

namespace FrontEnd.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        public async Task Authenticate(string returnUrl = "/")
        {
            await HttpContext.ChallengeAsync("Keycloak", new AuthenticationProperties
            {
                RedirectUri = returnUrl
            });
        }

        [Authorize]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("Keycloak", new AuthenticationProperties
            {
                RedirectUri = Url.Action("Index", "Home")
            });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            IEnumerable<User> users = await _userService.Index();

            users = users.OrderBy(u => u.Name);

            return View(users);
        }

        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Profile()
        {
            var user = await _userService.GetCurrentUser();
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost, Authorize(Roles = "Student"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(int id, User user)
        {
            if (id == 0 || id != user.Id) return NotFound();
            if (!ModelState.IsValid) {
                    TempData["Error"] = "Er ging iets mis tijdens het wijzigen van je profiel. Controleer of je alle velden juist hebt ingevuld.";
		    return RedirectToAction(nameof(Profile));
	    }

            var oldUser = await _userService.GetCurrentUser();
            user.Token = oldUser.Token;

            await _userService.EditUser(id, user);
            TempData["Alert"] = "Je profiel is gewijzigd!";

            return RedirectToAction(nameof(Profile));
        }

        [Authorize]
        public IActionResult Register()
        {
            var user = Library.Models.User.Create(HttpContext.User.Identity as ClaimsIdentity);
            return View(user);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize]
        public async Task<IActionResult> Register(User user)
        {
            if (!ModelState.IsValid) return RedirectToAction(nameof(Register));
            var claimedUser = Library.Models.User.Create(HttpContext.User.Identity as ClaimsIdentity);

            user.Email = claimedUser.Email;
            user.Token = claimedUser.Token;
            user.Name = claimedUser.Name;

            await _userService.Create(user);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            var currentUser = await _userService.GetCurrentUser();

            if (user.Id == currentUser.Id)
            {
                return RedirectToAction(nameof(Profile));
            }

            return View(user);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (id != user.Id) return NotFound();
            if (!ModelState.IsValid) return RedirectToAction(nameof(Edit));

            var oldUser = await _userService.GetById(id);
            user.Token = oldUser.Token;

            await _userService.Update(id, user);

            return RedirectToAction(nameof(Edit), new {id});
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize]
        public async Task<IActionResult> Delete(int userId)
        {
            var userToDelete = await _userService.GetById(userId);
            if (userToDelete == null) return NotFound();

            var claimedUser = Library.Models.User.Create(HttpContext.User.Identity as ClaimsIdentity);
            if (claimedUser.Token != userToDelete.Token)
            {
                if (!HttpContext.User.IsInRole("Admin"))
                {
                    return Unauthorized();
                }
            }

            await _userService.Delete(userToDelete.Id);

            if (claimedUser.Token == userToDelete.Token)
            {
                return RedirectToAction(nameof(Logout));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}