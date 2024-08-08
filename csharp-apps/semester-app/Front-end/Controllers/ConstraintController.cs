using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEnd.Services;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class ConstraintController : Controller
    {
        private readonly ConstraintService _constraintService;

        public ConstraintController(ConstraintService constraintService)
        {
            _constraintService = constraintService;
        }
        
        // Returns the constraints, ordered by name and passes it to the view.
        [Authorize(Roles="ModulePrincipal, ExamCommitteeMember")]
        public async Task<ActionResult> Index()
        {
            IEnumerable<Constraint> constraints = await _constraintService.Index();
            constraints = constraints.OrderBy(c => c.Name);
            return View(constraints);
        }

        // Returns a specific constraint by id and passes it to the view.
        [Authorize(Roles="ModulePrincipal, ExamCommitteeMember")]
        public async Task<ActionResult> Details(int id)
        {
            var constraint = await _constraintService.GetById(id);
            return View(constraint);
        }

        // redirects to the Create view.
        [Authorize(Roles="ModulePrincipal")]
        public IActionResult Create() => View();
        
        // POST-method to create a constraint.
        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles="ModulePrincipal")]
        public async Task<IActionResult> Create(Constraint constraint)
        {
            if (!ModelState.IsValid) return RedirectToAction(nameof(Create));

            await _constraintService.Create(constraint);
            return RedirectToAction(nameof(Index));
        }
        
        // Gets the constraint by id and passes it to the view
        [Authorize(Roles = "ModulePrincipal")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0) return NotFound();
            var constraint = await _constraintService.GetById(id);
            if (constraint == null) return NotFound();
            return View(constraint);
        }

        // POST-method to edit a constraint.
        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles="ModulePrincipal")]
        public async Task<IActionResult> Edit(int id, Constraint constraint)
        {
            if (id != constraint.Id) return NotFound();
            if (!ModelState.IsValid) return RedirectToAction(nameof(Edit));

            await _constraintService.Update(id, constraint);
            return RedirectToAction(nameof(Details), new {id});
        }

        /**
         * POST-request to delete the constraints.
         * Checks if the constraints are connected to a module.
         */
        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles="ModulePrincipal, ExamCommitteeMember")]
        public async Task<IActionResult> Delete(int id)
        {
            var constraint = await _constraintService.GetById(id);
            if (id != constraint.Id) return NotFound();

            if (constraint.ModuleConstraints.Count > 0) return RedirectToAction(nameof(Details), new {id});
            
            await _constraintService.Delete(constraint.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}