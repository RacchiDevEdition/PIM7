using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIM.Domain.Dtos;
using PIM.Domain.Entities;
using PIM.Domain.Enums;
using PIM.Infrastructure;
using System;
using System.Threading.Tasks;

namespace PIM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public StudentsController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _db.Students
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                .Include(s => s.ProgressRecords)
                .ToListAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var student = await _db.Students
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                .Include(s => s.ProgressRecords)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> Create(CreateStudentDto dto)
        {
            var student = new Student
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                Role = Role.Student,
                CreatedAt = DateTime.UtcNow
            };

            _db.Students.Add(student);
            await _db.SaveChangesAsync();

            return Ok(student);
        }

    }
}
