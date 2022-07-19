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
    public class AdherentsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public AdherentsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Adherents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Adherent>>> GetAdherent()
        {
          if (_context.Adherent == null)
          {
              return NotFound();
          }
            return await _context.Adherent.ToListAsync();
        }

        // GET: api/Adherents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Adherent>> GetAdherent(int id)
        {
          if (_context.Adherent == null)
          {
              return NotFound();
          }
            var adherent = await _context.Adherent.FindAsync(id);

            if (adherent == null)
            {
                return NotFound();
            }

            return adherent;
        }

        // PUT: api/Adherents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdherent(int id, Adherent adherent)
        {
            if (id != adherent.Id)
            {
                return BadRequest();
            }

            _context.Entry(adherent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdherentExists(id))
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

        // POST: api/Adherents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Adherent>> PostAdherent(AdherentSpecification adherentS)
        {
            Adherent adherent = new Adherent();
          if (_context.Adherent == null)
          {
              return Problem("Entity set 'MyDbContext.Adherent'  is null.");
          }
            //adherent.Id = adherentS.Id;
            adherent.Nom = adherentS.Nom;
            adherent.Prenom = adherentS.Prenom;
            adherent.DateNaissance = adherentS.DateNaissance;
            adherent.Payement = adherentS.Payement;
            adherent.Abonnement = _context.Find<Abonnement>(adherentS.Id);
            adherent.Compte = _context.Find<Compte>(adherentS.Id);
            if(adherent.Compte != null || adherent.Abonnement == null)
            {
                return Problem("Un element fournis est manqant");
            }

            _context.Adherent.Add(adherent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdherent", new { id = adherent.Id }, adherent);
        }

        [HttpPost("{adherentId}")]
        public async Task<ActionResult<Adherent>> PostAdherentNotification(int adherentId, Notification notification)
        {
            
            if (_context.Adherent == null)
            {
                return Problem("Entity set 'MyDbContext.Adherent'  is null.");
            }
            Adherent adherent = _context.Find<Adherent>(adherentId);
            if(adherent == null)
            {
                return Problem("Adherent n'existe pas.");
            }
            notification.Id = null;
            adherent.Notifications.Add(notification);
            _context.Entry(adherent).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdherent", new { id = adherent.Id }, adherent);
        }

        // DELETE: api/Adherents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdherent(int id)
        {
            if (_context.Adherent == null)
            {
                return NotFound();
            }
            var adherent = await _context.Adherent.FindAsync(id);
            if (adherent == null)
            {
                return NotFound();
            }

            _context.Adherent.Remove(adherent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdherentExists(int id)
        {
            return (_context.Adherent?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
