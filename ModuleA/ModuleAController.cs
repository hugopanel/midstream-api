using Microsoft.AspNetCore.Mvc;

namespace ModuleA;

[ApiController]
[Route("test")]
public class ModuleAController : ControllerBase
{
    [HttpGet("endpoint")]
    public async Task<IActionResult> Test()
    {
        return Ok("Welcome from Module A!");
    }
}