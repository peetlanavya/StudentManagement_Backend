using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentRepo.Data;
using StudentRepo.models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentRepo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly StudentDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(StudentDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDTO model)
        {
            if (model == null || string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest(new { message = "Username and Password required" });
            }

            var user = _context.Users.FirstOrDefault(u =>
                u.UserName == model.UserName &&
                u.Password == model.Password &&
                u.IsActive
            );

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid user credentials" });
            }

            var token = GenerateToken(user);

            return Ok(new
            {
                message = "Login success",
                token = token,
                userName = user.UserName,
                role = user.Role
            });
        }

        private string GenerateToken(AppUser user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var keyString = _config["JwtSettings:SecretKey"];

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(keyString)
            );

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}