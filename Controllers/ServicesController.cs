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
    [Route("api/[controller][action]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ServicesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Services
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetService()
        {
            if (_context.Service == null)
            {
                return NotFound();
            }
            return await _context.Service.Include(s => s.Cours).ToListAsync();
        }

        // GET: api/Services/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(int id)
        {
            if (_context.Service == null)
            {
                return NotFound();
            }
            var service = await _context.Service.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return service;
        }

        // PUT: api/Services/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(int id, Service service)
        {
            if (id != service.Id)
            {
                return BadRequest();
            }

            _context.Entry(service).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
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

        // POST: api/Services
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Service>> PostService(ServiceSpecification serviceS)
        {
            if (_context.Service == null)
            {
                return Problem("Entity set 'MyDbContext.Service'  is null.");
            }
            Service service = new Service();
            service.Nom = serviceS.Nom;
            _context.Service.Add(service);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetService", new { id = service.Id }, service);
        }

        [HttpPost("{serviceId}")]
        public async Task<ActionResult<Service>> PostService(int serviceId, CoursSpecification coursS)
        {
            if (_context.Service == null)
            {
                return Problem("Entity set 'MyDbContext.Service'  is null.");
            }
            Service service = _context.Find<Service>(serviceId);
            if (service == null)
            {
                return Problem("Le Service m'existe pas.");
            }
            Cours cours = new Cours();
            cours.Nom = coursS.Nom;
            service.Cours.Add(cours);
            _context.Entry(service).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetService", new { id = service.Id }, service);
        }

        [HttpPost("{serviceId},{coursId}")]
        public async Task<ActionResult<Service>> PostService(int serviceId, int coursId)
        {
            if (_context.Service == null)
            {
                return Problem("Entity set 'MyDbContext.Service'  is null.");
            }
            Service service = _context.Find<Service>(serviceId);
            Cours cours = _context.Find<Cours>(coursId);
            if (service == null || cours == null)
            {
                return Problem("Le Service m'existe pas.");
            }
            service.Cours.Add(cours);
            _context.Entry(service).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetService", new { id = service.Id }, service);
        }

        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            if (_context.Service == null)
            {
                return NotFound();
            }
            var service = await _context.Service.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            _context.Service.Remove(service);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceExists(int id)
        {
            return (_context.Service?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
