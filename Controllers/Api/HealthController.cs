using Microsoft.AspNetCore.Mvc;

namespace DliibApi.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("good");
    }
}