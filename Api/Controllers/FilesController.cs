using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Domain.Entities;
using MediatR;
using Application.Files.Queries;
using Application.Files;
using Domain.Permissions.File;


namespace Api.Controllers;

[ApiController]
[Route("Files")]
public class FilesController : ControllerBase
{
    private readonly ISender _mediator;

    public FilesController(ISender mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllFiles()
    {
        if (User.Claims.FirstOrDefault(c => c == FilePermissions.Read) is null)
        {
            return Unauthorized("You do not have permission to view files.");
        }
        
        try
        {
            var command = new GetAllFilesQuery();
            GetFilesResult result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            var errorMessage = ex.Message;
            return BadRequest(errorMessage);
        }
    }

}
