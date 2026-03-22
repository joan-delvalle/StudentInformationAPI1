using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentInformationAPI1.Data;
using StudentInformationAPI1.Models;

namespace StudentInformationAPI1.Controllers
{
    [ApiController]
    [Route("api/instructors")]
    public class InstructorsController : Controller
    {
       
            private readonly ApplicationDbContext _context;

            public InstructorsController(ApplicationDbContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<IActionResult> GetInstructors()
            {
                return Ok(await _context.Courses.ToListAsync());
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetInstructor(int id)
            {
                var instructor = await _context.Instructors.FindAsync(id);
                if (instructor == null) return NotFound();
                return Ok(instructor);
            }

            [HttpPost]
            public async Task<IActionResult> CreateInstructors([FromBody] Instructor instructor)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                await _context.Instructors.AddAsync(instructor);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetInstructor), new { id = instructor.InstructorId }, instructor);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateInstructor(Guid id, [FromBody] Instructor instructor)
            {
                if (id != instructor.InstructorId) return BadRequest();
                if (!ModelState.IsValid) return BadRequest(ModelState);

                _context.Entry(instructor).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Instructors.Any(i => i.InstructorId == id)) return NotFound();
                    throw;
                }
                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteInstructor(int id)
            {
                var instructor = await _context.Instructors.FindAsync(id);
                if (instructor == null) return NotFound();
                _context.Instructors.Remove(instructor);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }
}
