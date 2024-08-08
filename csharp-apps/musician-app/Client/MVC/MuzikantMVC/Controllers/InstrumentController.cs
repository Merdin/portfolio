using APISDK;
using Microsoft.AspNetCore.Mvc;
using MuzikantenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuzikantMVC.Controllers
{
    public class InstrumentController : Controller
    {
        private readonly IService<Muzikant> _muzikantService;
        private readonly IService<Instrument> _instrumentService;
        private readonly SessionManager _sessionManager;
        private readonly string _muzikantPath;
        private readonly string _instrumentPath;
        private readonly string _sessionKey;

        public InstrumentController(IService<Muzikant> muzikantService, IService<Instrument> instrumentService, SessionManager sessionManager)
        {
            _muzikantService = muzikantService;
            _instrumentService = instrumentService;
            _sessionManager = sessionManager;
            _muzikantPath = "/muzikant";
            _instrumentPath = "/instrument";
            _sessionKey = "muzikant";
        }

        // GET: VerzamelObject/CreateVerzamelObjectSpecial
        public async Task<ActionResult> CreateInstrumentSpecial(int id)
        {
            var muzikant = await _muzikantService.GetAsync(id, _muzikantPath);
            return View(new Instrument() { MuzikantId = muzikant.MuzikantId, Muzikant = muzikant });
        }

        // POST: VerzamelObject/CreateVerzamelObjectSpecial
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateInstrumentSpecial(int id, Instrument instrument)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var muzikant = await _muzikantService.GetAsync(instrument.MuzikantId, _muzikantPath);
                    _sessionManager.Set(HttpContext.Session, _sessionKey, $"{muzikant.Achternaam}, {muzikant.Voornaam} - {instrument.Naam} toegevoegd");
                    var result = await _instrumentService.AddAsync(instrument, _instrumentPath);
                    if (result)
                    {
                        return RedirectToAction("Index", nameof(Muzikant));
                    }
                }

                return View(instrument);
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Top3()
        {
            return View(await _instrumentService.GetAsync($"{_instrumentPath}/Top3"));
        }
    }
}
