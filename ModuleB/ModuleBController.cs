using Microsoft.AspNetCore.Mvc;

namespace ModuleB;

[ApiController]
[Route("test")]
public class ModuleBController : ControllerBase
{
    [HttpGet("endpoint")]
    public async Task<IActionResult> Test()
    {
        return Ok("Welcome from Module B!");
    }
}