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
    public class MuzikantController : ControllerBase
    {
        private readonly DefaultContext _context;

        public MuzikantController(DefaultContext context)
        {
            _context = context;
        }

        // GET: api/Muzikant
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Muzikant>>> GetMuzikant()
        {
            return await _context.Muzikant.Include(m => m.Instrumenten).ToListAsync();
        }

        // GET: api/Muzikant/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Muzikant>> GetMuzikant(int id)
        {
            var muzikant = await _context.Muzikant.FindAsync(id);

            if (muzikant == null)
            {
                return NotFound();
            }
            else
            {
                muzikant.Instrumenten = _context.Instrument.Where(i => i.MuzikantId == muzikant.MuzikantId).ToList();
            }

            return muzikant;
        }

        // PUT: api/Muzikant/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMuzikant(int id, Muzikant muzikant)
        {
            if (id != muzikant.MuzikantId)
            {
                return BadRequest();
            }

            _context.Entry(muzikant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MuzikantExists(id))
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

        // POST: api/Muzikant
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Muzikant>> PostMuzikant(Muzikant muzikant)
        {
            _context.Muzikant.Add(muzikant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMuzikant", new { id = muzikant.MuzikantId }, muzikant);
        }

        // DELETE: api/Muzikant/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Muzikant>> DeleteMuzikant(int id)
        {
            var muzikant = await _context.Muzikant.FindAsync(id);
            if (muzikant == null)
            {
                return NotFound();
            }

            _context.Muzikant.Remove(muzikant);
            await _context.SaveChangesAsync();

            return muzikant;
        }

        [HttpGet]
        [Route("/Muzikant/Actief")]
        public async Task<ActionResult<IEnumerable<Muzikant>>> Actief()
        {
            return await _context.Muzikant.Include(m => m.Instrumenten).Where(m => m.StopJaar == null).ToListAsync();
        }

        private bool MuzikantExists(int id)
        {
            return _context.Muzikant.Any(e => e.MuzikantId == id);
        }
    }
}
