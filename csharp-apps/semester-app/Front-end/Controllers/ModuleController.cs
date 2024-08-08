using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEnd.Services;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class ModuleController : Controller
    {
        private readonly ModuleService _moduleService;
        private readonly ConstraintService _constraintService;
        private readonly ModuleConstraintService _moduleConstraintService;

        // Constructor makes sure the services are reachable, the services make it possible to make api calls.
        public ModuleController(ModuleService moduleService, ConstraintService constraintService,
            ModuleConstraintService moduleConstraintService)
        {
            _moduleService = moduleService;
            _constraintService = constraintService;
            _moduleConstraintService = moduleConstraintService;
        }

        // Returns the modules, ordered by status and passes it to the view.
        [Authorize(Roles = "ModulePrincipal, ExamCommitteeMember")]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Module> modules = await _moduleService.Index();
            modules = modules.OrderBy(m => m.Status);
            return View(modules);
        }

        // Returns a specific module by id and passes it to the view.
        [Authorize(Roles = "ModulePrincipal, ExamCommitteeMember")]
        public async Task<IActionResult> Details(int id)
        {
            var module = await _moduleService.GetById(id);
            return View(module);
        }

        // Gets all the constraints to make them available in the create view and redirects to the view.
        [Authorize(Roles = "ModulePrincipal")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Constraints = await _constraintService.Index();
            return View();
        }

        /**
 * POST-method to create a module.
 * Checks model state and if Expiration date is set to the future.
 * Sets the Status to draft by default.
 * Adds the constraints if the array is not null.
 */
        [HttpPost, Authorize(Roles = "ModulePrincipal"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Module module, int[] constraintIds)
        {
            if (!ModelState.IsValid) return RedirectToAction(nameof(Create));
            if (module.Expiration <= DateTime.Today) return RedirectToAction(nameof(Create));

            module.Status = ModuleStatus.Draft;
            module.Visible = false;

            var moduleResult = await _moduleService.Create(module);
            foreach (var constraintId in constraintIds)
            {
                await _moduleConstraintService.Create(new ModuleConstraint()
                    {ModuleId = moduleResult.Id, ConstraintId = constraintId});
            }

            return RedirectToAction(nameof(Index));
        }

        /**
 * Gets the module by id and passes it to the view
 * Gets all the constraints to make them available in the edit view
 */
        [Authorize(Roles = "ModulePrincipal")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0) return NotFound();
            var module = await _moduleService.GetById(id);
            if (module == null) return NotFound();

            ViewBag.Constraints = await _constraintService.Index();
            return View(module);
        }

        /**
 * POST-method to edit a module.
 * Checks model state and if Expiration date is set to the future.
 * Sets the Status to draft by default.
 * Deletes all the constraints and adds them again.
 */
        [HttpPost, Authorize(Roles = "ModulePrincipal"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Module module, int[] constraintIds)
        {
            if (id != module.Id) return NotFound();
            if (module.Expiration <= DateTime.Today) return RedirectToAction(nameof(Edit));
            if (!ModelState.IsValid) return RedirectToAction(nameof(Edit));

            await _moduleConstraintService.Delete(module.Id);

            foreach (var constraintId in constraintIds)
            {
                await _moduleConstraintService.Create(new ModuleConstraint()
                    {ModuleId = module.Id, ConstraintId = constraintId});
            }

            module.Status = ModuleStatus.Draft;
            module.Visible = false;

            await _moduleService.Update(id, module);
            return RedirectToAction(nameof(Details), new {id});
        }

        // Sets the Status to checked
        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "ModuleManager")]
        public async Task<IActionResult> ApproveModule(int id)
        {
            var module = await _moduleService.GetById(id);
            if (id != module.Id) return NotFound();

            module.Status = ModuleStatus.Checked;

            await _moduleService.Update(id, module);
            return RedirectToAction(nameof(Details), new {id});
        }

        // Sets the Status to Invalid
        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "ModuleManager")]
        public async Task<IActionResult> DeclineModule(int id)
        {
            var module = await _moduleService.GetById(id);
            if (id != module.Id) return NotFound();

            module.Status = ModuleStatus.Invalid;
            module.Visible = false;

            await _moduleService.Update(id, module);
            return RedirectToAction(nameof(Details), new {id});
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "ModuleManager")]
        public async Task<IActionResult> ToggleVisibility(int id)
        {
            var module = await _moduleService.GetById(id);
            if (id != module.Id) return NotFound();

            module.Visible = !module.Visible.Equals(true);

            await _moduleService.Update(id, module);
            return RedirectToAction(nameof(Details), new {id});
        }


        // POST-request to delete the constraints connected to the module and the module itself.
        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "ExamCommitteeMember")]
        public async Task<IActionResult> Delete(int id)
        {
            var module = await _moduleService.GetById(id);
            if (id != module.Id) return NotFound();

            await _moduleConstraintService.Delete(module.Id);
            await _moduleService.Delete(module.Id);

            return RedirectToAction(nameof(Index));
        }
    }
}