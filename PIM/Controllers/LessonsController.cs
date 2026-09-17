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
    public class LessonsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public LessonsController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lessons = await _db.Lessons
                .Include(l => l.Contents)
                .ToListAsync();
            return Ok(lessons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var lesson = await _db.Lessons
                .Include(l => l.Contents)
                .FirstOrDefaultAsync(l => l.Id == id);
            if (lesson == null) return NotFound();
            return Ok(lesson);
        }

        [HttpGet("bycourse/{courseId}")]
        public async Task<IActionResult> GetByCourse(Guid courseId)
        {
            var lessons = await _db.Lessons
                .Include(l => l.Contents)
                .Where(l => l.CourseId == courseId)
                .ToListAsync();
            return Ok(lessons);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Lesson lesson)
        {
            if (lesson == null) return BadRequest();

            // verify course exists
            var courseExists = await _db.Courses.AnyAsync(c => c.Id == lesson.CourseId);
            if (!courseExists) return BadRequest(new { error = "Course not found for CourseId" });

            _db.Lessons.Add(lesson);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = lesson.Id }, lesson);
        }
    }
}
