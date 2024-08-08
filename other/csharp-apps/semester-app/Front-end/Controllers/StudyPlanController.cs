using System;
using System.Linq;
using System.Threading.Tasks;
using FrontEnd.Services;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class StudyPlanController : Controller
    {
        private readonly StudyPlanService _studyPlanService;
        private readonly ModuleService _moduleService;
        private readonly LearningUnitService _learningUnitService;
        private readonly UserService _userService;

        public StudyPlanController(StudyPlanService studyPlanService, ModuleService moduleService,
            LearningUnitService learningUnitService, UserService userService)
        {
            _studyPlanService = studyPlanService;
            _moduleService = moduleService;
            _learningUnitService = learningUnitService;
            _userService = userService;
        }

        [Authorize(Roles = "StudyCareerCounselor")]
        public async Task<IActionResult> Index()
        {
            ViewBag.StudyPlans = await _studyPlanService.Index();
            return View();
        }

        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Create()
        {
            var learningUnits = await _learningUnitService.Index();
            ViewBag.Semesters = _learningUnitService;
            ViewBag.ExploringIt = learningUnits.Where(x => x.Type == UnitType.ExploringIt).ToList();
            ViewBag.Profielkeuze = learningUnits.Where(x => x.Type == UnitType.Profielkeuze).ToList();
            ViewBag.VerbredendeNiveau3 = learningUnits.Where(x => x.Type == UnitType.VerbredendeNiveau3).ToList();
            ViewBag.VerdiependeNiveau3 = learningUnits.Where(x => x.Type == UnitType.VerdiependeNiveau3).ToList();
            ViewBag.Overig = learningUnits.Where(x => x.Type == UnitType.Overig).ToList();
            ViewBag.Reperatiesemester = learningUnits.Where(x => x.Type == UnitType.Reperatiesemester).ToList();
            ViewBag.modules = await _moduleService.GetActive();
            return View();
        }

        [HttpPost, Authorize(Roles = "Student")]
        public async Task Create(StudyPlan studyPlan)
        {
            studyPlan.User = await _userService.GetCurrentUser();
            studyPlan.ExploringIt =
                await _learningUnitService.GetById(int.Parse(HttpContext.Request.Form["exploringIt"]));
            studyPlan.ProfileChoiceOne =
                await _learningUnitService.GetById(int.Parse(HttpContext.Request.Form["profileChoiceOne"]));
            studyPlan.ProfileChoiceTwo =
                await _learningUnitService.GetById(int.Parse(HttpContext.Request.Form["profileChoiceTwo"]));
            studyPlan.FreeChoiceOne =
                await _learningUnitService.GetById(int.Parse(HttpContext.Request.Form["freeChoiceOne"]));
            studyPlan.FreeChoiceTwo =
                await _learningUnitService.GetById(int.Parse(HttpContext.Request.Form["freeChoiceTwo"]));
            studyPlan.FreeChoiceThree =
                await _learningUnitService.GetById(int.Parse(HttpContext.Request.Form["freeChoiceThree"]));
            await _studyPlanService.Create(studyPlan);
        }

        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Studyplan = await _studyPlanService.GetById(id);
            return View();
        }

        [Authorize(Roles = "Student")]
        public async Task<IActionResult> FinishStudyPlan(int id)
        {
            var studyPlan = await _studyPlanService.GetById(id);
            TempData["Alert"] = "Je studieroute is nu zichtbaar voor je SLB'er!";
            return RedirectToAction(nameof(Edit), new {id});
        }

        [Authorize(Roles = "StudyCareerCounselor, ExamCommitteeMember")]
        public async Task<IActionResult> ApproveStudyPlan(int id)
        {
            var studyPlan = await _studyPlanService.GetById(id);
            // Approve logic
            TempData["Alert"] = "De studieroute is nu goedgekeurd!";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "StudyCareerCounselor, ExamCommitteeMember")]
        public async Task<IActionResult> DenyStudyPlan(int id)
        {
            var studyPlan = await _studyPlanService.GetById(id);
            // Deny logic
            TempData["Alert"] = "De studieroute is nu afgewezen!";
            return RedirectToAction(nameof(Index));
        }
    }
}