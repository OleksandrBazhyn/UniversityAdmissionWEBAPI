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
    public class AdmissionRequestsController : ControllerBase
    {
        private readonly UniversityAdmissionAPIContext _context;
        private readonly string _connectionstring;

        public AdmissionRequestsController(UniversityAdmissionAPIContext context)
        {
            _context = context;
        }

        // GET: api/AdmissionRequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdmissionRequest>>> GetAdmissionRequests()
        {
            var admissionRequests = await (from ar in _context.AdmissionRequests
                                           join e in _context.Entrants on ar.EntrantID equals e.Id
                                           join u in _context.Universities on ar.UniversityID equals u.Id
                                           select new
                                           {
                                               Id = ar.Id,
                                               UniversityName = u.Name,
                                               EntrantName = $"{e.FirstName} {e.LastName}"
                                           }).ToListAsync();

            return Ok(admissionRequests);
        }

        // GET: api/AdmissionRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdmissionRequest>> GetAdmissionRequest(int id)
        {
            var admissionrequest = await _context.AdmissionRequests.FindAsync(id);

            if (admissionrequest == null)
            {
                return NotFound();
            }

            var admissionRequests = await (from ar in _context.AdmissionRequests
                                           join e in _context.Entrants on ar.EntrantID equals e.Id
                                           join u in _context.Universities on ar.UniversityID equals u.Id
                                           where ar.Id == id
                                           select new
                                           {
                                               Id = ar.Id,
                                               UniversityName = u.Name,
                                               EntrantName = $"{e.FirstName} {e.LastName}"
                                           }).ToListAsync();

            return Ok(admissionRequests);
        }

        // PUT: api/AdmissionRequests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdmissionRequest(int id, AdmissionRequest admissionRequest)
        {
            if (id != admissionRequest.Id)
            {
                return BadRequest();
            }

            _context.Entry(admissionRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdmissionRequestExists(id))
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

        // POST: api/AdmissionRequests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdmissionRequest>> PostAdmissionRequest(AdmissionRequest admissionRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AdmissionRequests.Add(admissionRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdmissionRequest", new { id = admissionRequest.Id }, admissionRequest);
        }

        // DELETE: api/AdmissionRequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmissionRequest(int id)
        {
            var admissionRequest = await _context.AdmissionRequests.FindAsync(id);
            if (admissionRequest == null)
            {
                return NotFound();
            }

            _context.AdmissionRequests.Remove(admissionRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdmissionRequestExists(int id)
        {
            return _context.AdmissionRequests.Any(e => e.Id == id);
        }
    }
}
