using LibraryAPI.DataBase;
using LibraryAPI.Models;
using LibraryAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _config;
        public AuthController(AppDbContext appDbContext,IConfiguration config)
        {
            _appDbContext = appDbContext;
            _config = config;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDTO registerDTO)
        {
            var email = registerDTO.Email;
            if (_appDbContext.Users.Any(u => u.Email == email))
            {
                return BadRequest("Email already exists");
            }
            var password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password);
            var user = new User
            {
                Name = registerDTO.Name,
                Email = registerDTO.Email,
                PasswordHash = password,
                Role = registerDTO.Role
            };
            _appDbContext.Users.Add(user);
            _appDbContext.SaveChanges();
            return StatusCode(201, "User registered successfully");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            var user = _appDbContext.Users.FirstOrDefault(u => u.Email == loginDTO.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid email or password");
            }
            var token = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {

                    new System.Security.Claims.Claim("email", user.Email),
                    new System.Security.Claims.Claim("role", user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var securityToken = token.CreateToken(tokenDescriptor);
            return Ok(new { token = token.WriteToken(securityToken) });


        }
    }
}
