using APISDK;
using Microsoft.AspNetCore.Mvc;
using MuzikantenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuzikantMVC.Controllers
{
    public class MuzikantController : Controller
    {
        private readonly IService<Muzikant> _muzikantService;
        private readonly SessionManager _sessionManager;
        private readonly string _muzikantPath;
        private readonly string _sessionKey;

        public MuzikantController(IService<Muzikant> muzikantService, SessionManager sessionManager)
        {
            _muzikantService = muzikantService;
            _sessionManager = sessionManager;
            _muzikantPath = "/muzikant";
            _sessionKey = "muzikant";
        }

        // GET: Verzamelaar
        public async Task<ActionResult> Index()
        {
            ViewBag.SessieInfo = _sessionManager.Get(HttpContext.Session, _sessionKey);
            return View(await _muzikantService.GetAsync(_muzikantPath));
        }

        // GET: Verzamelaar/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await _muzikantService.GetAsync(id, _muzikantPath));
        }

        // GET: Verzamelaar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Verzamelaar/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Muzikant muzikant)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _sessionManager.Set(HttpContext.Session, _sessionKey, $"{muzikant.Achternaam}, {muzikant.Voornaam} aangemaakt");
                    var result = await _muzikantService.AddAsync(muzikant, _muzikantPath);
                    if (result)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

                return View(muzikant);
            }
            catch
            {
                return View();
            }
        }

        // GET: Verzamelaar/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _muzikantService.GetAsync(id, _muzikantPath));
        }

        // POST: Verzamelaar/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Muzikant muzikant)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _sessionManager.Set(HttpContext.Session, _sessionKey, $"{muzikant.Achternaam}, {muzikant.Voornaam} is gewijzigd");
                    var result = await _muzikantService.UpdateAsync(id, muzikant, _muzikantPath);
                    if (result)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

                return View(muzikant);
            }
            catch
            {
                return View();
            }
        }

        // GET: Verzamelaar/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _muzikantService.GetAsync(id, _muzikantPath));
        }

        // POST: Verzamelaar/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Muzikant muzikant)
        {
            try
            {
                var v = await _muzikantService.GetAsync(id, _muzikantPath);
                _sessionManager.Set(HttpContext.Session, _sessionKey, $"{v.Achternaam}, {v.Voornaam} is verwijderd");
                var result = await _muzikantService.DeleteAsync(id, _muzikantPath);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> ActieveMuzikanten()
        {
            return View(await _muzikantService.GetAsync($"{_muzikantPath}/Actief"));
        }
    }
}
