using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Application.Teams;
using Application.Teams.Commands;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Api.Controllers;

[ApiController]
[Route("Team")]
public class TeamController : ControllerBase
{
    private readonly ISender _mediator;

    public TeamController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreateTeam")]
    public async Task<IActionResult> CreateTeam(CreateTeamRequest request)
    {
        try
        {
            var command = new CreateTeamCommand(request.Name, request.ProjectId);

            TeamResult result = await _mediator.Send(command);

            var users = request.Users;

            for(int i = 0; i < users.Count; i++)
            {
                var memberCommand = new CreateMemberCommand(users[i].UserId, result.Team.Id.ToString());
                MemberResult memberResult = await _mediator.Send(memberCommand);
                for (int j = 0; j < users[i].RolesId.Count; j++)
                {
                    var roleCommand = new CreateMemberRoleCommand(memberResult.Member.Id.ToString(), users[i].RolesId[j]);
                    MemberRoleResult roleResult = await _mediator.Send(roleCommand);
                }
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            var errorMessage = new AuthenticationResponseMessage("Error during the creation of the team.");
            return BadRequest(errorMessage);
        }
    }

    [HttpPost("CreateProject")]
    public async Task<IActionResult> CreateProject(CreateProjectRequest request)
    {
        try
        {
            var command = new CreateProjectCommand(request.Name, request.Description);

            ProjectResult result = await _mediator.Send(command);

            return Ok(result);
        }
        catch (Exception ex)
        {
            var errorMessage = new AuthenticationResponseMessage("Error during the creation of the project.");
            return BadRequest(errorMessage);
        }
    }

    [HttpPost("CreateRole")]
    public async Task<IActionResult> CreateRole(CreateRoleRequest request)
    {
        try
        {
            var command = new CreateRoleCommand(request.Name);

            RoleResult result = await _mediator.Send(command);

            return Ok(result);
        }
        catch (Exception ex)
        {
            var errorMessage = new AuthenticationResponseMessage("Error during the creation of the role.");
            return BadRequest(errorMessage);
        }
    }

    [HttpPost("CreateMember")]
    public async Task<IActionResult> CreateMember(CreateMemberRequest request)
    {
        try
        {
            var memberCommand = new CreateMemberCommand(request.UserId, request.TeamId.ToString());
            MemberResult memberResult = await _mediator.Send(memberCommand);
            for (int i = 0; i < request.RolesId.Count; i++)
            {
                var roleCommand = new CreateMemberRoleCommand(memberResult.Member.Id.ToString(), request.RolesId[i]);
                MemberRoleResult roleResult = await _mediator.Send(roleCommand);
            }

            return Ok(memberResult);
        }
        catch (Exception ex)
        {
            var errorMessage = new AuthenticationResponseMessage("Error during the creation of the project.");
            return BadRequest(errorMessage);
        }
    }

}