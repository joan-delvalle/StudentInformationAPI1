using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentInformationAPI1.Data;
using StudentInformationAPI1.Models;

namespace StudentInformationAPI1.Controllers
{
    [ApiController]
    [Route("api/departments")]
    public class DepartmentsController : Controller
    {
        
            private readonly ApplicationDbContext _context;

            public DepartmentsController(ApplicationDbContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<IActionResult> GetDepartment()
            {
                return Ok(await _context.Courses.ToListAsync());
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetDepartment(int id)
            {
                var department = await _context.Courses.FindAsync(id);
                if (department == null) return NotFound();
                return Ok(department);
            }

            [HttpPost]
            public async Task<IActionResult> CreateDepartment([FromBody] Department department)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                await _context.Departments.AddAsync(department);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetDepartment), new { id = department.DepartmentId }, department);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateDepartment(Guid id, [FromBody] Department department)
            {
                if (id != department.DepartmentId) return BadRequest();
                if (!ModelState.IsValid) return BadRequest(ModelState);

                _context.Entry(department).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Departments.Any(d => d.DepartmentId == id)) return NotFound();
                    throw;
                }
                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteDepartment(int id)
            {
                var department = await _context.Departments.FindAsync(id);
                if (department == null) return NotFound();
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
                return NoContent();
            }

        }
    }
