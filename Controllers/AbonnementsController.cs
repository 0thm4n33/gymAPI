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
    public class AbonnementsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public AbonnementsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Abonnements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Abonnement>>> GetAbonnement()
        {
          if (_context.Abonnement == null)
          {
              return NotFound();
          }
            return await _context.Abonnement.Include(a => a.Services).ToListAsync();
        }

        // GET: api/Abonnements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Abonnement>> GetAbonnement(int id)
        {
          if (_context.Abonnement == null)
          {
              return NotFound();
          }
            var abonnement = await _context.Abonnement.FindAsync(id);

            if (abonnement == null)
            {
                return NotFound();
            }

            return abonnement;
        }

        // PUT: api/Abonnements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAbonnement(int id, AbonnementSpecification abonnementS)
        {
            if (id != abonnementS.Id)
            {
                return BadRequest();
            }
            Abonnement abonnement = new Abonnement();
            abonnement.Id = id;
            abonnement.dateDebut = abonnementS.dateDebut;
            abonnement.dateFin = abonnementS.dateFin;
            abonnement.Montant = abonnementS.Montant;
            abonnement.Designation = abonnementS.Designation;

            _context.Entry(abonnement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AbonnementExists(id))
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

        // POST: api/Abonnements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Abonnement>> PostAbonnement(AbonnementSpecification abonnementS)
        {
          if (_context.Abonnement == null)
          {
              return Problem("Entity set 'MyDbContext.Abonnement'  is null.");
          }
            Abonnement abonnement = new Abonnement();

            abonnement.Designation = abonnementS.Designation;
            abonnement.dateDebut = abonnementS.dateDebut;
            abonnement.dateFin = abonnementS.dateFin;
            abonnement.Montant = abonnementS.Montant;
            _context.Abonnement.Add(abonnement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAbonnement", new { id = abonnement.Id }, abonnement);
        }

        [HttpPost("{abonnementId},{serviceId}")]
        public async Task<ActionResult<Abonnement>> PostAbonnementService(int abonnementId, int serviceId)
        {
            if (_context.Abonnement == null)
            {
                return Problem("Entity set 'MyDbContext.Abonnement'  is null.");
            }
            Service service = _context.Find<Service>(serviceId);
            Abonnement abonnement = _context.Find<Abonnement>(abonnementId);
            if(abonnement == null || service == null)
            {
                return Problem("L'abonnement n'existe pas.");
            }
            
            abonnement.Services.Add(service);
            _context.Entry(abonnement).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAbonnement", new { id = abonnement.Id }, abonnement);
        }
        // DELETE: api/Abonnements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbonnement(int id)
        {
            if (_context.Abonnement == null)
            {
                return NotFound();
            }
            var abonnement = await _context.Abonnement.FindAsync(id);
            if (abonnement == null)
            {
                return NotFound();
            }

            _context.Abonnement.Remove(abonnement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AbonnementExists(int id)
        {
            return (_context.Abonnement?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
