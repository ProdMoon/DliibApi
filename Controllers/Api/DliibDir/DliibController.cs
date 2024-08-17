using DliibApi.Dtos;
using DliibApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DliibApi.Controllers.Api.DliibDir;

[ApiController]
[Route("api/[controller]")]
public class DliibController(DliibService dliibService, UserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DliibDto>>> GetDliibs()
    {
        return Ok(await dliibService.GetAllDliibDtos(User.Identity?.Name));
    }

    [HttpGet("my")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<DliibDto>>> GetMyDliibs()
    {
        if (User.Identity == null)
        {
            return Unauthorized();
        }

        return Ok(await dliibService.GetUserDliibDtos(User.Identity.Name));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DliibDto>> GetDliib(int id)
    {
        return Ok(await dliibService.GetDliibDto(id, User.Identity?.Name));
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> PutDliib(DliibDto dliibDto)
    {
        var dliib = await dliibService.GetDliib(dliibDto.Id);
        if (dliib == null)
        {
            return NotFound();
        }

        if (User.Identity == null)
        {
            return Unauthorized();
        }

        var user = await userService.GetUserByName(User.Identity.Name);
        if (dliib.Author != user)
        {
            return Unauthorized();
        }

        await dliibService.UpdateDliib(dliibDto);

        return NoContent();
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<DliibDto>> PostDliib(DliibDto dliibDto)
    {
        if (User.Identity == null)
        {
            return Unauthorized();
        }
        var createdDliibDto = await dliibService.CreateDliib(dliibDto, User.Identity.Name);

        return CreatedAtAction("GetDliib", new { id = dliibDto.Id }, createdDliibDto);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteDliib(int id)
    {
        var dliib = await dliibService.GetDliib(id);
        if (dliib == null)
        {
            return NotFound();
        }

        if (User.Identity == null)
        {
            return Unauthorized();
        }

        var user = await userService.GetUserByName(User.Identity.Name);
        if (dliib.Author != user)
        {
            return Unauthorized();
        }

        await dliibService.DeleteDliib(id);

        return NoContent();
    }
}