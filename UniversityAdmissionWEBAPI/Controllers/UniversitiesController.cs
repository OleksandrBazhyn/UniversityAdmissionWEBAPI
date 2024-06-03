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
    public class UniversitiesController : ControllerBase
    {
        private readonly UniversityAdmissionAPIContext _context;

        public UniversitiesController(UniversityAdmissionAPIContext context)
        {
            _context = context;
        }

        // GET: api/Universities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<University>>> GetUniversities()
        {
            var universities = await (from un in _context.Universities
                                      select new
                                      {
                                          Id = un.Id,
                                          Name = un.Name,
                                          WebSiteLink = un.WebSiteLink,
                                          AvgUniverityAdmissionGrade = un.AvgUniverityAdmissionGrade,
                                          Description = un.Description
                                      }).ToListAsync();

            return Ok(universities);
        }

        // GET: api/Universities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<University>> GetUniversity(int id)
        {
            var university = await _context.Universities.FindAsync(id);

            if (university == null)
            {
                return NotFound();
            }

            var universities = await (from un in _context.Universities
                                      where un.Id == id
                                      select new
                                      {
                                          Id = un.Id,
                                          Name = un.Name,
                                          WebSiteLink = un.WebSiteLink,
                                          AvgUniverityAdmissionGrade = un.AvgUniverityAdmissionGrade,
                                          Description = un.Description
                                      }).ToListAsync();

            return Ok(universities);
        }

        // PUT: api/Universities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUniversity(int id, University university)
        {
            if (id != university.Id)
            {
                return BadRequest();
            }

            _context.Entry(university).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UniversityExists(id))
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

        // POST: api/Universities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<University>> PostUniversity(University university)
        {
            _context.Universities.Add(university);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUniversity", new { id = university.Id }, university);
        }

        // DELETE: api/Universities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUniversity(int id)
        {
            var university = await _context.Universities.FindAsync(id);
            if (university == null)
            {
                return NotFound();
            }

            _context.Universities.Remove(university);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UniversityExists(int id)
        {
            return _context.Universities.Any(e => e.Id == id);
        }
    }
}
