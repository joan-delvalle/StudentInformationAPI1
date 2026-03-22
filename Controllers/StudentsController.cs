using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentInformationAPI1.Data;
using StudentInformationAPI1.Models;

namespace StudentInformationAPI1.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : Controller
    {
            private readonly ApplicationDbContext _context;

            public StudentsController(ApplicationDbContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<IActionResult> GetStudents()
            {
                return Ok(await _context.Students.ToListAsync());
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetStudent(int id)
            {
                var student = await _context.Students.FindAsync(id);
                if (student == null) return NotFound();
                return Ok(student);
            }

            [HttpPost]
            public async Task<IActionResult> CreateStudent([FromBody] Student student)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetStudent), new { id = student.StudentId }, student);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateStudent(Guid id, [FromBody] Student student)
            {
                if (id != student.StudentId) return BadRequest();
                if (!ModelState.IsValid) return BadRequest(ModelState);

                _context.Entry(student).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Students.Any(s => s.StudentId == id)) return NotFound();
                    throw;
                }
                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteStudent(int id)
            {
                var student = await _context.Students.FindAsync(id);
                if (student == null) return NotFound();
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }
}
