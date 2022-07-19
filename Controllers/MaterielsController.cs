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
    public class MaterielsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public MaterielsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Materiels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Materiel>>> Getmateriels()
        {
          if (_context.materiels == null)
          {
              return NotFound();
          }
            return await _context.materiels.ToListAsync();
        }

        // GET: api/Materiels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Materiel>> GetMateriel(int id)
        {
          if (_context.materiels == null)
          {
              return NotFound();
          }
            var materiel = await _context.materiels.FindAsync(id);

            if (materiel == null)
            {
                return NotFound();
            }

            return materiel;
        }

        // PUT: api/Materiels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMateriel(int id, Materiel materiel)
        {
            if (id != materiel.Id)
            {
                return BadRequest();
            }

            _context.Entry(materiel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterielExists(id))
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

        // POST: api/Materiels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Materiel>> PostMateriel(Materiel materiel)
        {
          if (_context.materiels == null)
          {
              return Problem("Entity set 'MyDbContext.materiels'  is null.");
          }
            materiel.Id = null;
            _context.materiels.Add(materiel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMateriel", new { id = materiel.Id }, materiel);
        }

        // DELETE: api/Materiels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMateriel(int id)
        {
            if (_context.materiels == null)
            {
                return NotFound();
            }
            var materiel = await _context.materiels.FindAsync(id);
            if (materiel == null)
            {
                return NotFound();
            }

            _context.materiels.Remove(materiel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MaterielExists(int id)
        {
            return (_context.materiels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
