using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Domain.Entities;
using MediatR;

using Application.Files;
using Api.Models.Files;
using Application.Files.Commands;
using Application.Files.Queries;


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
    [HttpGet("GetListByProject")]
    public async Task<IActionResult> GetFiles(string projectId)
    {
        try
        {
            Console.WriteLine("hello");
            var command = new GetFilesQuery(projectId);
            GetFilesResult result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            var errorMessage = ex.Message;
            return BadRequest(errorMessage);
        }
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetFile(string fileId)
    {
        try
        {
            var command = new GetFileQuery(fileId);
            GetFileResult result = await _mediator.Send(command);

            // Convert file byte array to a stream and return it
            var fileStream = new MemoryStream(result.File);
            return new FileStreamResult(fileStream, "application/octet-stream")
            {
                FileDownloadName = result.FileApp.name // Assuming FileApp has a FileName property
            };
        }
        catch (Exception ex)
        {
            var errorMessage = ex.Message;
            return BadRequest(errorMessage);
        }
    }

    [HttpPost("Upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadFile([FromForm] UploadFileRequest request)
    {
        try
        {
            var command = new UploadFileCommand(request.File,request.Description,request.Belong);
            UploadFileResult result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            var errorMessage = ex.Message;
            return BadRequest(errorMessage);
        }
    }

}
