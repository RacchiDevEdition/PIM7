using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIM.Infrastructure;
using PIM.Domain.Entities;

namespace PIM.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly AppDbContext _db;
        public UsersController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var users = await _db.Set<User>().ToListAsync();
            return View(users);
        }

        public IActionResult Create()
        {
            // show simple form; the form posts name/email/password
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] string Name, [FromForm] string Email, [FromForm] string Password)
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ModelState.AddModelError(string.Empty, "Todos os campos são obrigatórios");
                return View();
            }

            // cria um Student por padrão; para criar Admin, use o Account seeding
            var user = new Student
            {
                Name = Name,
                Email = Email
            };

            // hash password via UserService to keep logic consistent
            var userService = HttpContext.RequestServices.GetRequiredService<PIM.Models.Interfaces.IUserService>();
            userService.Create(user, Password);

            return RedirectToAction(nameof(Index));
        }
    }
}
