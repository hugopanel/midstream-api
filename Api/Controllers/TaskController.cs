using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Application.Tasks;
using Application.Tasks.Commands;
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

    [HttpGet("GetTasksToDisplay")]
    public async Task<IActionResult> GetTasksToDisplay(string projectId)
    {
        try
        {
            var query = new GetTasksToDisplayQuery(projectId);
            ListTasksToDisplayResult result = await _mediator.Send(query);

            return Ok(result);
        }
        catch (Exception ex)
        {
            var errorMessage = new AuthenticationResponseMessage("Error during the get of the tasks.");
            return BadRequest(errorMessage);
        }
    }

    [HttpGet("GetTasksByProject")]
    public async Task<IActionResult> GetTasksByProject(string projectId)
    {
        try
        {
            var query = new GetTasksByProjectQuery(projectId);
            ListTasksResult result = await _mediator.Send(query);

            return Ok(result);
        }
        catch (Exception ex)
        {
            var errorMessage = new AuthenticationResponseMessage("Error during the get of the tasks.");
            return BadRequest(errorMessage);
        }
    }

    [HttpGet("GetTasksByAssignee")]
    public async Task<IActionResult> GetTasksByAssignee(string projectId, string userId)
    {
        try
        {
            var query = new GetTasksByAssigneeQuery(projectId, userId);
            ListTasksResult result = await _mediator.Send(query);

            return Ok(result);
        }
        catch (Exception ex)
        {
            var errorMessage = new AuthenticationResponseMessage("Error during the get of the tasks.");
            return BadRequest(errorMessage);
        }
    }

    [HttpGet("GetTasksByType")]
    public async Task<IActionResult> GetTasksByType(string projectId, string type)
    {
        try
        {
            var query = new GetTasksByTypeQuery(projectId, type);
            ListTasksResult result = await _mediator.Send(query);

            return Ok(result);
        }
        catch (Exception ex)
        {
            var errorMessage = new AuthenticationResponseMessage("Error during the get of the tasks.");
            return BadRequest(errorMessage);
        }
    }

    [HttpGet("GetTasksByEndDate")]
    public async Task<IActionResult> GetTasksByEndDate(string projectId, string endDate)
    {
        try
        {
            DateTime convertedEndDate = DateTime.ParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var query = new GetTasksByEndDateQuery(projectId, convertedEndDate);
            ListTasksResult result = await _mediator.Send(query);

            return Ok(result);
        }
        catch (Exception ex)
        {
            var errorMessage = new AuthenticationResponseMessage("Error during the get of the tasks.");
            return BadRequest(errorMessage);
        }
    }

    [HttpGet("GetTasksByPriority")]
    public async Task<IActionResult> GetTasksByPriority(string projectId, string priority)
    {
        try
        {
            var query = new GetTasksByPriorityQuery(projectId, priority);
            ListTasksResult result = await _mediator.Send(query);

            return Ok(result);
        }
        catch (Exception ex)
        {
            var errorMessage = new AuthenticationResponseMessage("Error during the get of the tasks.");
            return BadRequest(errorMessage);
        }
    }

    [HttpGet("GetTaskToEdit")]
    public async Task<IActionResult> GetTaskToEdit(string taskId)
    {
        try
        {
            var query = new GetTaskToEditQuery(taskId);
            TaskToEditResult result = await _mediator.Send(query);

            return Ok(result);
        }
        catch (Exception ex)
        {
            var errorMessage = new AuthenticationResponseMessage("Error during the get of the tasks.");
            return BadRequest(errorMessage);
        }
    }

    [Authorize]
    [HttpPost("CreateTask")]
    public async Task<IActionResult> CreateTask(CreateTaskRequest request)
    {
        try
        {
            var Id = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value);
            var command = new CreateTaskCommand(DateTime.ParseExact(request.BeginningDate, "dd/MM/yyyy", CultureInfo.InvariantCulture), DateTime.ParseExact(request.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture), request.Priority, request.Status, request.TypeOfTask, request.Title, request.Description, request.Belong, Id.ToString(), request.AssignedTo, request.RelatedTo);
            TaskResult result = await _mediator.Send(command);

            return Ok(result);
        }
        catch (Exception ex)
        {
            var errorMessage = new AuthenticationResponseMessage(ex.Message);
            return BadRequest(errorMessage);
        }
    }

    [HttpPost("UpdateTasks")]
    public async Task<IActionResult> UpdateTasks(UpdateTasksRequest request)
    {
        try
        {
            var command = new UpdateTasksCommand(request.tasks);
            MessageResult result = await _mediator.Send(command);

            return Ok(result);
        }
        catch (Exception ex)
        {
            var errorMessage = new AuthenticationResponseMessage("Error during the get of the tasks.");
            return BadRequest(errorMessage);
        }
    }
}