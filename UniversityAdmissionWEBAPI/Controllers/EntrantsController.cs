using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityAdmissionWEBAPI.Models;

namespace UniversityAdmissionWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntrantsController : ControllerBase
    {
        private readonly UniversityAdmissionAPIContext _context;

        public EntrantsController(UniversityAdmissionAPIContext context)
        {
            _context = context;
        }

        // GET: api/Entrants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entrant>>> GetEntrants()
        {
            var entrants = await (from en in _context.Entrants
                                         select new
                                         {
                                             Id = en.Id,
                                             FirstName = en.FirstName,
                                             LastName = en.LastName,
                                             NationalExamGrade = en.NationalExamGrade,
                                             IsPrivileged = en.IsPrivileged
                                         }).ToListAsync();
            return Ok(entrants);
        }

        // GET: api/Entrants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Entrant>> GetEntrant(int id)
        {
            var entrant = await _context.Entrants.FindAsync(id);

            if (entrant == null)
            {
                return NotFound();
            }

            var entrants = await (from en in _context.Entrants
                                  where en.Id == id
                                  select new
                                  {
                                      Id = en.Id,
                                      FirstName = en.FirstName,
                                      LastName = en.LastName,
                                      NationalExamGrade = en.NationalExamGrade,
                                      IsPrivileged = en.IsPrivileged
                                  }).ToListAsync();

            return Ok(entrants);
        }

        // PUT: api/Entrants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntrant(int id, Entrant entrant)
        {
            if (id != entrant.Id)
            {
                return BadRequest();
            }

            _context.Entry(entrant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntrantExists(id))
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

        // POST: api/Entrants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Entrant>> PostEntrant(Entrant entrant)
        {
            _context.Entrants.Add(entrant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntrant", new { id = entrant.Id }, entrant);
        }

        // DELETE: api/Entrants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntrant(int id)
        {
            var entrant = await _context.Entrants.FindAsync(id);
            if (entrant == null)
            {
                return NotFound();
            }

            _context.Entrants.Remove(entrant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntrantExists(int id)
        {
            return _context.Entrants.Any(e => e.Id == id);
        }
    }
}
