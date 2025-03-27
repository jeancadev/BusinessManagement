using BusinessManagement.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessManagement.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase    
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthController(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            Console.WriteLine($"[DEBUG] Login user: {request.Username}, pass: '{request.Password}'");
            // 1) Busca el usuario en la base de datos
            var user = await _userRepository.GetByUsernameAsync(request.Username);
            
            if (user == null)
            {
                Console.WriteLine("[DEBUG] Usuario no existe");
                return Unauthorized("Usuario no existe");
            }

            Console.WriteLine($"[DEBUG] request.Password = '{request.Password}' (length = {request.Password.Length})");
            // 2) Verifica la contraseña con BCrypt
            bool isValid = BCrypt.Net.BCrypt.Verify(request.Password, user.HashedPassword);
            Console.WriteLine($"[DEBUG] BCrypt.Verify('{request.Password}', '{user.HashedPassword}') => {isValid}");

            if (!isValid)
            {
                Console.WriteLine("[DEBUG] Contraseña inválida");
                return Unauthorized("Contraseña inválida");
            }

            // 3) Genera el token con su rol 
            var tokenString = GenerateJwtToken(user.Username, user.Role);

            return Ok(new { token = tokenString });
        }

        [Authorize]
        [HttpGet("it-secret")]
        public IActionResult ItSecret()
        {
            // Cualquier token válido puede acceder, 
            // si deseo solo el rol "IT", puedo utilizar [Authorize(Roles = "IT")]
            return Ok("Solo personal con un token válido puede acceder a este secreto.");
        }

        // Método que acepta (username, role) para asignar el rol real del usuario
        private string GenerateJwtToken(string username, string role)
        {
            // Obtiene la clave secreta del appsettings
            var secretKey = _configuration["JwtSettings:Secret"] ?? "ClaveUltraSecreta12345";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Crea los claims basicos incluyendo el rol del usuario
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role),
                // En caso de probar con un claim personalizado por ejemplo "Department"
                new Claim("Department", "IT"),
                new Claim("Level", "Senior")
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // Expira en 1 hora
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
