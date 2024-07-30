using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace DliibApi.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public HttpStatusCode Get()
    {
        return HttpStatusCode.OK;
    }
}