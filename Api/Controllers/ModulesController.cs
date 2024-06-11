using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Domain.Entities;
using MediatR;
using Application.Modules.Queries;
using Application.Modules;


namespace Api.Controllers;

[ApiController]
[Route("Modules")]
public class ModulesController : ControllerBase
{
    private readonly ISender _mediator;

    public ModulesController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllModules()
    {
        try
        {
            var command = new GetAllModulesQuery();
            GetModulesResult result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            var errorMessage = ex.Message;
            return BadRequest(errorMessage);
        }
    }

    [HttpPost("GetRecommended")]
    public async Task<IActionResult> GetRecommendedModules(GetRecommendedModulesRequest request)
    {
        try
        {
            Console.WriteLine("GetRecommendedModules");
            var command = new GetRecommendedModulesQuery(request.descriptionRequest);
            GetModulesResult result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            var errorMessage = ex.Message;
            return BadRequest(errorMessage);
        }
    }

}
