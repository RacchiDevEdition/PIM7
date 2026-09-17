using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIM.Infrastructure;
using PIM.Domain.Entities;

namespace PIM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _db;
        public CoursesController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _db.Courses
                .Include(c => c.Lessons)
                .Include(c => c.Tags)
                .ToListAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var course = await _db.Courses
                .Include(c => c.Lessons)
                .Include(c => c.Tags)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (course == null) return NotFound();
            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Course course)
        {
            _db.Courses.Add(course);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = course.Id }, course);
        }
    }
}
