using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Application.Tasks;
using Application.Tasks.Queries;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Api.Controllers;

[ApiController]
[Route("Task")]
public class TaskController : ControllerBase
{
    private readonly ISender _mediator;

    public TaskController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetTasks")]
    public async Task<IActionResult> GetTasks()
    {
        try
        {
            var query = new GetTasksQuery();
            ListTasksResult result = await _mediator.Send(query);

            return Ok(result);
        }
        catch (Exception ex)
        {
            var errorMessage = new AuthenticationResponseMessage("Error during the get of the tasks.");
            return BadRequest(errorMessage);
        }
    }
}