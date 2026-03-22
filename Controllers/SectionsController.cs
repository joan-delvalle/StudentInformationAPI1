using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentInformationAPI1.Data;
using StudentInformationAPI1.Models;

namespace StudentInformationAPI1.Controllers
{
    [ApiController]
    [Route("api/sections")]
    public class SectionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetSections()
        {
            return Ok(await _context.Courses.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSection(int id)
        {
            var section = await _context.Sections.FindAsync(id);
            if (section == null) return NotFound();
            return Ok(section);
        }

        [HttpPost]
        public async Task<IActionResult> Createsection([FromBody] Section section)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _context.Sections.AddAsync(section);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSection), new { id = section.SectionId }, section);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSection(Guid id, [FromBody] Section section)
        {
            if (id != section.SectionId) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Entry(section).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Sections.Any(s => s.SectionId == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSection(int id)
        {
            var section = await _context.Sections.FindAsync(id);
            if (section == null) return NotFound();
            _context.Sections.Remove(section);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
