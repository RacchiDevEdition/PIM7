using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PIM.Infrastructure;

namespace PIM.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _db;
        public DashboardController(AppDbContext db) => _db = db;

        public IActionResult Index()
        {
            var vm = new PIM.Areas.Admin.Models.DashboardViewModel
            {
                UsersCount = _db.Set<PIM.Domain.Entities.User>().Count(),
                StudentsCount = _db.Students.Count(),
                TeachersCount = _db.Teachers.Count(),
                CoursesCount = _db.Courses.Count(),
                LessonsCount = _db.Lessons.Count(),
                CurrentUserName = User?.Identity?.Name
            };

            return View(vm);
        }
    }
}
