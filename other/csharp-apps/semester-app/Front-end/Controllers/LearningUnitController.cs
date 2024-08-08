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
    public class LearningUnitController : Controller
    {
        private readonly LearningUnitService _learningUnitService;
        private readonly ModuleService _moduleService;
        private readonly LearningUnitModuleService _learningUnitModuleService;

        public LearningUnitController(LearningUnitService learningUnitService,
            LearningUnitModuleService learningUnitModuleService, ModuleService moduleService)
        {
            _learningUnitService = learningUnitService;
            _learningUnitModuleService = learningUnitModuleService;
            _moduleService = moduleService;
        }

        [Authorize(Roles = "ModulePrincipal, ModuleManager")]
        public async Task<IActionResult> Index()
        {
            IEnumerable<LearningUnit> learningUnits = await _learningUnitService.Index();
            learningUnits = learningUnits.OrderBy(m => m.Status);
            return View(learningUnits);
        }

        [Authorize(Roles = "ModulePrincipal, ModuleManager")]
        public async Task<IActionResult> Details(int id)
        {
            var semester = await _learningUnitService.GetById(id);
            return View(semester);
        }

        [Authorize(Roles = "ModulePrincipal, ModuleManager")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Modules = await _moduleService.GetActive();
            return View();
        }

        [HttpPost, Authorize(Roles = "ModulePrincipal, ModuleManager"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LearningUnit learningUnit, int[] moduleIds)
        {
            // if (!ModelState.IsValid) return RedirectToAction(nameof(Create));
            if (learningUnit.Expiration <= DateTime.Today) return RedirectToAction(nameof(Create));
            learningUnit.Status = ModuleStatus.Draft;

            var newLearningUnit = await _learningUnitService.Create(learningUnit);

            foreach (var moduleId in moduleIds)
            {
                await _learningUnitModuleService.Create(
                    new LearningUnitModule {LearningUnitId = newLearningUnit.Id, ModuleId = moduleId});
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "ModulePrincipal")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0) return NotFound();
            var learningUnit = await _learningUnitService.GetById(id);
            if (learningUnit == null) return NotFound();
            ViewBag.Modules = await _moduleService.Index();

            return View(learningUnit);
        }

        [HttpPost, Authorize(Roles = "ModulePrincipal"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LearningUnit learningUnit, int[] moduleIds)
        {
            if (id != learningUnit.Id) return NotFound();
            if (learningUnit.Expiration <= DateTime.Today) return RedirectToAction(nameof(Edit));
            if (!ModelState.IsValid) return RedirectToAction(nameof(Edit));

            await _learningUnitModuleService.Delete(learningUnit.Id);

            foreach (var ModuleId in moduleIds)
            {
                await _learningUnitModuleService.Create(
                    new LearningUnitModule {LearningUnitId = learningUnit.Id, ModuleId = ModuleId});
            }

            learningUnit.Status = ModuleStatus.Draft;

            await _learningUnitService.Update(id, learningUnit);
            return RedirectToAction(nameof(Details), new {id});
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "ModuleManager")]
        public async Task<IActionResult> ApproveLearningUnit(int id)
        {
            var learningUnit = await _learningUnitService.GetById(id);
            if (id != learningUnit.Id) return NotFound();

            learningUnit.Status = ModuleStatus.Checked;

            await _learningUnitService.Update(id, learningUnit);
            return RedirectToAction(nameof(Details), new {id});
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "ModuleManager")]
        public async Task<IActionResult> DenyLearningUnit(int id)
        {
            var learningUnit = await _learningUnitService.GetById(id);
            if (id != learningUnit.Id) return NotFound();

            learningUnit.Status = ModuleStatus.Invalid;

            await _learningUnitService.Update(id, learningUnit);
            return RedirectToAction(nameof(Details), new {id});
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "ModuleManager")]
        public async Task<IActionResult> Delete(int id)
        {
            var learningUnit = await _learningUnitService.GetById(id);
            if (id != learningUnit.Id) return NotFound();

            await _learningUnitModuleService.Delete(learningUnit.Id);
            await _learningUnitService.Delete(learningUnit.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}