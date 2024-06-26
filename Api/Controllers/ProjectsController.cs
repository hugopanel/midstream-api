using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Domain.Entities;
using MediatR;
using Application.Projects.Queries;
using Application.Projects;


namespace Api.Controllers;

[ApiController]
[Route("Projects")]
public class ProjectsController : ControllerBase
{
    private readonly ISender _mediator;

    public ProjectsController(ISender mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllProjects()
    {
        try
        {
            var command = new GetAllProjectsQuery();
            GetProjectsResult result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            var errorMessage = ex.Message;
            return BadRequest(errorMessage);
        }
    }

}
