using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webAPi.Models;

namespace webAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialitesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public SpecialitesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Specialites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Specialite>>> GetSpecialite()
        {
          if (_context.Specialite == null)
          {
              return NotFound();
          }
            return await _context.Specialite.ToListAsync();
        }

        // GET: api/Specialites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Specialite>> GetSpecialite(int id)
        {
          if (_context.Specialite == null)
          {
              return NotFound();
          }
            var specialite = await _context.Specialite.FindAsync(id);

            if (specialite == null)
            {
                return NotFound();
            }

            return specialite;
        }

        // PUT: api/Specialites/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecialite(int id, Specialite specialite)
        {
            if (id != specialite.Id)
            {
                return BadRequest();
            }

            _context.Entry(specialite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecialiteExists(id))
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

        // POST: api/Specialites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Specialite>> PostSpecialite(Specialite specialite)
        {
          if (_context.Specialite == null)
          {
              return Problem("Entity set 'MyDbContext.Specialite'  is null.");
          }
            specialite.Id = null;
            _context.Specialite.Add(specialite);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpecialite", new { id = specialite.Id }, specialite);
        }

        // DELETE: api/Specialites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialite(int id)
        {
            if (_context.Specialite == null)
            {
                return NotFound();
            }
            var specialite = await _context.Specialite.FindAsync(id);
            if (specialite == null)
            {
                return NotFound();
            }

            _context.Specialite.Remove(specialite);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpecialiteExists(int id)
        {
            return (_context.Specialite?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
