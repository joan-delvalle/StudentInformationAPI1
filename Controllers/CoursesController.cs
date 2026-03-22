using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentInformationAPI1.Data;
using StudentInformationAPI1.Models;

namespace StudentInformationAPI1.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CoursesController : Controller
    {
        
            private readonly ApplicationDbContext _context;

            public CoursesController(ApplicationDbContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<IActionResult> GetCourses()
            {
                return Ok(await _context.Courses.ToListAsync());
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetCourse(int id)
            {
                var course = await _context.Courses.FindAsync(id);
                if (course == null) return NotFound();
                return Ok(course);
            }

            [HttpPost]
            public async Task<IActionResult> CreateCourse([FromBody] Course course)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                await _context.Courses.AddAsync(course);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetCourse), new { id = course.CourseId }, course);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateCourse(Guid id, [FromBody] Course course)
            {
                if (id != course.CourseId) return BadRequest();
                if (!ModelState.IsValid) return BadRequest(ModelState);

                _context.Entry(course).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Courses.Any(c => c.CourseId == id)) return NotFound();
                    throw;
                }
                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteCourse(int id)
            {
                var course = await _context.Courses.FindAsync(id);
                if (course == null) return NotFound();
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
                return NoContent();
            }

        }
    }
