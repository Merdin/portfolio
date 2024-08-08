using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MuzikantenApi.Data;
using MuzikantenApi.Models;

namespace MuzikantenApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InstrumentController : ControllerBase
    {
        private readonly DefaultContext _context;

        public InstrumentController(DefaultContext context)
        {
            _context = context;
        }

        // GET: api/Instrument
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instrument>>> GetInstrument()
        {
            return await _context.Instrument.Include(i => i.Muzikant).ToListAsync();
        }

        // GET: api/Instrument/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Instrument>> GetInstrument(int id)
        {
            var instrument = await _context.Instrument.FindAsync(id);

            if (instrument == null)
            {
                return NotFound();
            }
            else
            {
                instrument.Muzikant = await _context.Muzikant.FindAsync(instrument.MuzikantId);
            }

            return instrument;
        }

        // PUT: api/Instrument/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstrument(int id, Instrument instrument)
        {
            if (id != instrument.InstrumentId)
            {
                return BadRequest();
            }

            _context.Entry(instrument).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstrumentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Instrument
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Instrument>> PostInstrument(Instrument instrument)
        {
            _context.Instrument.Add(instrument);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInstrument", new { id = instrument.InstrumentId }, instrument);
        }

        // DELETE: api/Instrument/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Instrument>> DeleteInstrument(int id)
        {
            var instrument = await _context.Instrument.FindAsync(id);
            if (instrument == null)
            {
                return NotFound();
            }

            _context.Instrument.Remove(instrument);
            await _context.SaveChangesAsync();

            return instrument;
        }

        [HttpGet]
        [Route("/Instrument/Top3")]
        public async Task<ActionResult<IEnumerable<Instrument>>> Top3()
        {
            return await _context.Instrument.Include(i => i.Muzikant).OrderByDescending(i => i.Waarde).Take(3).ToListAsync();
        }

        private bool InstrumentExists(int id)
        {
            return _context.Instrument.Any(e => e.InstrumentId == id);
        }
    }
}
