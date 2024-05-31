
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Application.Services;
using Api.Models;
using Domain.Entities;
using Infrastructure.Data;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IConfiguration _configuration;
        private readonly UserDbContext _context;

        public AccountController(UserService userService, IConfiguration configuration, UserDbContext context)
        {
            _userService = userService;
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            try
            {
                var token = _userService.GenerateConfirmationToken(request.Username, request.Password, request.Email);
                await _userService.SendConfirmationEmailAsync(request.Email, token);
                return Ok("Registration successful. Please check your email to confirm your account.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmEmail(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateLifetime = true
            };

            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                var username = claimsPrincipal.FindFirst("username")?.Value;
                var password = claimsPrincipal.FindFirst("password")?.Value;
                var email = claimsPrincipal.FindFirst("EmailAddress")?.Value;

                // Create the user in the database using extracted information
                var user = new User
                {
                    Username = username,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                    Email = email
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok("Email confirmed and user registered.");
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid token.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var isValid = await _userService.ValidateUserAsync(request.Username, request.Password);
            if (!isValid)
                return Unauthorized();

            // Generate JWT token
            var token = GenerateJwtToken(request.Username);
            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpPost("forgot_password")]
        public async Task<IActionResult> Forgot_Password(RegisterRequest request)
        {
            try
            {
                var token = _userService.GenerateConfirmationToken(request.Username, request.Password, request.Email);
                await _userService.SendConfirmationEmailAsync(request.Email, token);
                return Ok("Registration successful. Please check your email to confirm your account.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize] // Requires authentication
        [HttpGet("profile")]
        public IActionResult Profile()
        {
            var username = User.Identity.Name;

            return Ok(new { Username = username });
        }
    }
}
