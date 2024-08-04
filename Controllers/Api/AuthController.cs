using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DliibApi.Data;
using DliibApi.Dtos;

namespace DliibApi.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class AuthController(UserManager<DliibUser> userManager, SignInManager<DliibUser> signInManager) : ControllerBase
{
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return Ok();
    }

    [HttpGet("getSession")]
    [Authorize]
    public async Task<IActionResult> GetSession()
    {
        var userName = User.Identity.Name;
        var user = await userManager.FindByNameAsync(userName);
        return Ok(user);
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] SignupDto model)
    {
        if (ModelState.IsValid)
        {
            var user = new DliibUser
            {
                UserName = model.Email,
                Email = model.Email,
                NickName = model.NickName
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //await signInManager.SignInAsync(user, isPersistent: false);
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors.First().Description);
            }
        }

        return BadRequest("Invalid model");
    }

    [HttpDelete("deleteAccount")]
    [Authorize]
    public async Task<IActionResult> DeleteAccount()
    {
        var userName = User.Identity.Name;
        var user = await userManager.FindByNameAsync(userName);
        await signInManager.SignOutAsync();
        await userManager.DeleteAsync(user);
        return Ok();
    }
}
