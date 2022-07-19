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
    [Route("api/[controller]")]
    [ApiController]
    public class MoniteursController : ControllerBase
    {
        private readonly MyDbContext _context;

        public MoniteursController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Moniteurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Moniteur>>> GetMoniteur()
        {
          if (_context.Moniteur == null)
          {
              return NotFound();
          }
            return await _context.Moniteur.ToListAsync();
        }

        // GET: api/Moniteurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Moniteur>> GetMoniteur(int id)
        {
          if (_context.Moniteur == null)
          {
              return NotFound();
          }
            var moniteur = await _context.Moniteur.FindAsync(id);

            if (moniteur == null)
            {
                return NotFound();
            }

            return moniteur;
        }

        // PUT: api/Moniteurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoniteur(int id, Moniteur moniteur)
        {
            if (id != moniteur.Id)
            {
                return BadRequest();
            }

            _context.Entry(moniteur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoniteurExists(id))
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

        // POST: api/Moniteurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Moniteur>> PostMoniteur(MoniteurSpecification moniteurS)
        {
          if (_context.Moniteur == null)
          {
              return Problem("Entity set 'MyDbContext.Moniteur'  is null.");
          }
            Moniteur moniteur = new Moniteur();
            moniteur.Nom = moniteurS.Nom;
            moniteur.Prenom = moniteurS.Prenom;
            moniteur.Compte = _context.Find<Compte>(moniteurS.Compte);
            if(moniteur.Compte == null)
            {
                return Problem("Ce compte n'existe pas.");
            }
            _context.Moniteur.Add(moniteur);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMoniteur", new { id = moniteur.Id }, moniteur);
        }

        [HttpPost("{idMoniteur}, {idSpecialite}")]
        public async Task<ActionResult<Moniteur>> PostMoniteurSpecialite(int idMoniteur, int idSpecialite)
        {
            if (_context.Moniteur == null)
            {
                return Problem("Entity set 'MyDbContext.Moniteur'  is null.");
            }
            Moniteur moniteur = _context.Find<Moniteur>(idMoniteur);
            Specialite specialite = _context.Find<Specialite>(idSpecialite);
            if(moniteur == null || specialite == null) 
            {
                return Problem("Ce Moniteur ou la specialite n'existe pas.");
            }
                
            moniteur.Specialites.Add(specialite);
            _context.Entry(moniteur).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMoniteur", new { id = moniteur.Id }, moniteur);
        }
        // DELETE: api/Moniteurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoniteur(int id)
        {
            if (_context.Moniteur == null)
            {
                return NotFound();
            }
            var moniteur = await _context.Moniteur.FindAsync(id);
            if (moniteur == null)
            {
                return NotFound();
            }

            _context.Moniteur.Remove(moniteur);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MoniteurExists(int id)
        {
            return (_context.Moniteur?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
