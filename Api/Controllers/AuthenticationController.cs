using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Api.Models;
using Application.Authentication;
using Application.Authentication.Commands;
using Application.Authentication.Queries;
using Domain.Entities;
using Contracts;
using MediatR;

namespace Api.Controllers;

[ApiController]
[Route("Auth")]
public class AuthenticationController : ControllerBase
{
    private readonly ISender _mediator;

    public AuthenticationController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterQuery(request.Username, request.FirstName, request.LastName, request.Email);
        AuthenticationResult result = await _mediator.Send(command);

        return Ok("Registration successful. Please check your email to confirm your account.");
    }

    [Authorize]
    [HttpPost("ConfirmRegistration")]
    public async Task<IActionResult> ConfirmRegistration(ConfirmRequest request)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(request.Token);

        string username = jwtSecurityToken.Claims.First(claim => claim.Type == "username").Value;
        string firstName = jwtSecurityToken.Claims.First(claim => claim.Type == "firstName").Value;
        string lastName = jwtSecurityToken.Claims.First(claim => claim.Type == "lastName").Value;
        string email = jwtSecurityToken.Claims.First(claim => claim.Type == "email").Value;

        var command = new ConfirmRegistrationCommand(username, firstName, lastName, email, request.Password);
        AuthenticationResult result = await _mediator.Send(command);

        var response = new AuthenticationResponse(result.User.Id, result.User.Username, result.User.FirstName,
            result.User.LastName, result.User.Email, result.Token);

        return Ok(response);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = new LoginQuery(request.Username, request.Password);
        AuthenticationResult result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
    {
        var query = new ResetPasswordQuery(request.Email);
        await _mediator.Send(query);

        return Ok();
    }

    [Authorize]
    [HttpPost("ConfirmResetPassword")]
    public async Task<IActionResult> ConfirmResetPassword(ConfirmResetPasswordRequest request)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(request.Token);

        string id = jwtSecurityToken.Claims.First(claim => claim.Type == "userid").Value;

        var command = new ConfirmPasswordResetCommand(id, request.NewPassword);
        await _mediator.Send(command);

        return Ok("New password set successfully. Please login with your new password.");
    }

    [Authorize]
    [HttpGet("Profile")]
    public IActionResult Profile()
    {
        var username = User.Identity.Name;

        return Ok(new { Username = username });
    }
}