using System;
using Microsoft.AspNetCore.Mvc;
using PIM.Models.Auth;
using PIM.Domain.Entities;
using PIM.Domain.Enums;
using PIM.Models.Interfaces;

namespace PIM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtTokenService _jwt;

        public AuthController(IUserService userService, IJwtTokenService jwt)
        {
            _userService = userService;
            _jwt = jwt;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.Password) || string.IsNullOrWhiteSpace(req.Email))
                return BadRequest("Email and password are required.");

            User user = req.Role == Role.Teacher
                ? new Teacher { Name = req.Name, Email = req.Email, Role = req.Role }
                : new Student { Name = req.Name, Email = req.Email, Role = req.Role };

            var created = _userService.Create(user, req.Password);
            return CreatedAtAction(null, new { id = created.Id }, new { created.Id, created.Name, created.Email, created.Role });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest req)
        {
            var user = _userService.Authenticate(req.Email, req.Password);
            if (user == null) return Unauthorized();

            var token = _jwt.GenerateToken(user);
            return Ok(new AuthResponse { Token = token, ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(60) });
        }
    }
}
