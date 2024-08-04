using DliibApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DliibApi.Controllers.Api.DliibDir;

[ApiController]
[Route("api/[controller]")]
public class DliibLikeController(DliibLikeService dliibLikeService) : ControllerBase
{
    [HttpPost("like/{dliibId}")]
    [Authorize]
    public async Task<IActionResult> LikeDliib(int dliibId)
    {
        if (User.Identity == null)
        {
            return Unauthorized();
        }

        var userName = User.Identity.Name;
        await dliibLikeService.ToggleLike(dliibId, userName);

        return NoContent();
    }

    [HttpPost("dislike/{dliibId}")]
    [Authorize]
    public async Task<IActionResult> DislikeDliib(int dliibId)
    {
        if (User.Identity == null)
        {
            return Unauthorized();
        }

        var userName = User.Identity.Name;
        await dliibLikeService.ToggleDislike(dliibId, userName);

        return NoContent();
    }
}