using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using BCrypt.Net;

namespace Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(username);
            if (user == null)
                return false;

            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }

        public async Task RegisterUserAsync(string username, string password, string email)
        {
            var user = new User
            {
                Username = username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Email = email
            };

            await _userRepository.RegisterUserAsync(user);
        }

        public string GenerateConfirmationToken(string username, string password, string email)
        {
            var claims = new[]
            {
            new Claim("username", username),
            new Claim("password", password),
            new Claim("EmailAddress", email)
            };

            Console.WriteLine(email);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // Set expiration time for token
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task SendConfirmationEmailAsync(string email, string token)
        {
            var fromAddress = new MailAddress("midstream42@gmail.com", "Midstream");
            var toAddress = new MailAddress(email);
            var fromPassword = "njoewmtozlrhoyzl";
            var subject = "Confirm Your Email";
            var body = $"Click the following link to confirm your email: http://localhost:5101/api/account/confirm?token={token}";


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                await smtp.SendMailAsync(message);
            }
        }

        public string GenerateResetPasswordToken(string email)
        {
            var claims = new[]
            {
            new Claim("EmailAddress", email)
            };

            Console.WriteLine(email);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // Set expiration time for token
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task SendResetEmailAsync(string email, string token)
        {
            var fromAddress = new MailAddress("midstream42@gmail.com", "Midstream");
            var toAddress = new MailAddress(email);
            var fromPassword = "njoewmtozlrhoyzl";
            var subject = "Reset your password";
            var body = $"Click the following link to reset your password: http://localhost:5101/api/account/confirm_reset?token={token}";


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                await smtp.SendMailAsync(message);
            }
        }
    }
}
