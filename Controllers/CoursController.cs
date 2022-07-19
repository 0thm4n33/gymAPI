using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webAPi.Models;
using webAPi.ResourceModels;

namespace webAPi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CoursController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CoursController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Cours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cours>>> GetCours()
        {
          if (_context.Cours == null)
          {
              return NotFound();
          }
            return await _context.Cours.ToListAsync();
        }

        // GET: api/Cours/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cours>> GetCours(int id)
        {
          if (_context.Cours == null)
          {
              return NotFound();
          }
            var cours = await _context.Cours.FindAsync(id);

            if (cours == null)
            {
                return NotFound();
            }

            return cours;
        }

        // PUT: api/Cours/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCours(int id, Cours cours)
        {
            if (id != cours.Id)
            {
                return BadRequest();
            }

            _context.Entry(cours).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoursExists(id))
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

        // POST: api/Cours
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cours>> PostCours(CoursSpecification coursS)
        {
          if (_context.Cours == null)
          {
              return Problem("Entity set 'MyDbContext.Cours'  is null.");
          }
            Cours cours = new Cours();
            cours.Nom = coursS.Nom;
            cours.Duree = coursS.Duree;
            cours.Niveau = coursS.Niveau;
            _context.Cours.Add(cours);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCours", new { id = cours.Id }, cours);
        }

        [HttpPost("{idCours}, {idMateriel}")]
        public async Task<ActionResult<Cours>> PostCoursMateriel(int idCours, int idMateriel)
        {
            if (_context.Cours == null)
            {
                return Problem("Entity set 'MyDbContext.Cours'  is null.");
            }

            Cours cours = _context.Find<Cours>(idCours);
            Materiel materiel = _context.Find<Materiel>(idMateriel);
            if(cours == null || materiel == null)
            {
                return Problem("Ce cours ou bien ce materiel n'existe pas.");
            }
            cours.Materiels.Add(materiel);
            _context.Entry(cours).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCours", new { id = cours.Id }, cours);
        }

        [HttpPost("{idCours}")]
        public async Task<ActionResult<Cours>> PostCoursTemps(int idCours, Temps temps)
        {
            if (_context.Cours == null)
            {
                return Problem("Entity set 'MyDbContext.Cours'  is null.");
            }

            Cours cours = _context.Find<Cours>(idCours);
            if (cours == null)
            {
                return Problem("Ce temps n'existe pas.");
            }
            cours.EmploiTemps.Add(temps);
            _context.Entry(cours).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCours", new { id = cours.Id }, cours);
        }

        [HttpPost("{idCours}, {idTemps}")]
        public async Task<ActionResult<Cours>> PostCoursTemps(int idCours, int idTemps)
        {
            if (_context.Cours == null)
            {
                return Problem("Entity set 'MyDbContext.Cours'  is null.");
            }

            Cours cours = _context.Find<Cours>(idCours);
            Temps temps = _context.Find<Temps>(idTemps);
            if (cours == null || temps == null)
            {
                return Problem("Ce temps ou bien le temps n'existe pas.");
            }
            cours.EmploiTemps.Add(temps);
            _context.Entry(cours).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCours", new { id = cours.Id }, cours);
        }

        // DELETE: api/Cours/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCours(int id)
        {
            if (_context.Cours == null)
            {
                return NotFound();
            }
            var cours = await _context.Cours.FindAsync(id);
            if (cours == null)
            {
                return NotFound();
            }

            _context.Cours.Remove(cours);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CoursExists(int id)
        {
            return (_context.Cours?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
