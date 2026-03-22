using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentInformationAPI1.Data;
using StudentInformationAPI1.Models;

namespace StudentInformationAPI1.Controllers
{

    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : Controller
    {
        
            private readonly ApplicationDbContext _context;

            public EnrollmentsController(ApplicationDbContext context)
            {
                _context = context;
            }
            [HttpGet]
            public async Task<IActionResult> GetEnrollments()
            {
                return Ok(await _context.Enrollments.ToListAsync());
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetEnrollment(int id)
            {
                var enrollment = await _context.Enrollments.FindAsync(id);
                if (enrollment == null) return NotFound();
                return Ok(enrollment);
            }

            [HttpPost]
            public async Task<IActionResult> CreateEnrollment([FromBody] Enrollment enrollment)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                await _context.Enrollments.AddAsync(enrollment);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetEnrollment), new { id = enrollment.EnrollmentId }, enrollment);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateEnrollment(Guid id, [FromBody] Enrollment enrollment)
            {
                if (id != enrollment.EnrollmentId) return BadRequest();
                if (!ModelState.IsValid) return BadRequest(ModelState);

                _context.Entry(enrollment).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Enrollments.Any(e => e.EnrollmentId == id)) return NotFound();
                    throw;
                }
                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteEnrollment(int id)
            {
                var enrollment = await _context.Enrollments.FindAsync(id);
                if (enrollment == null) return NotFound();
                _context.Enrollments.Remove(enrollment);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }
}
