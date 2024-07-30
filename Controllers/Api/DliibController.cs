using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DliibApi.Data;
using Microsoft.AspNetCore.Authorization;
using DliibApi.Models;

namespace DliibApi.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class DliibController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DliibModel>>> GetDliibs()
    {
        return await db.Dliibs
            .Include(x => x.Author)
            .OrderByDescending(x => x.Id)
            .Select(x => new DliibModel
            {
                Id = x.Id,
                Content = x.Content,
                Likes = x.Likes,
                Dislikes = x.Dislikes,
                CreatedAt = x.CreatedAt,
                AuthorNickName = x.Author != null ? x.Author.NickName : "익명"
            })
            .ToListAsync();
    }

    [HttpGet("my")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<DliibModel>>> GetMyDliibs()
    {
        if (User.Identity == null)
        {
            return Unauthorized();
        }
        var user = await db.Users.FirstOrDefaultAsync(x => x.NormalizedUserName == User.Identity.Name);
        return await db.Dliibs
            .Where(x => x.Author == user)
            .OrderByDescending(x => x.Id)
            .Select(x => new DliibModel
            {
                Id = x.Id,
                Content = x.Content,
                Likes = x.Likes,
                Dislikes = x.Dislikes,
                CreatedAt = x.CreatedAt,
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Dliib>> GetDliib(int id)
    {
        var dliib = await db.Dliibs.FindAsync(id);

        if (dliib == null)
        {
            return NotFound();
        }

        return dliib;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDliib(int id, Dliib dliib)
    {
        if (id != dliib.Id)
        {
            return BadRequest();
        }

        db.Entry(dliib).State = EntityState.Modified;

        try
        {
            await db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DliibExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Dliib>> PostDliib(Dliib dliib)
    {
        if (User.Identity == null)
        {
            return Unauthorized();
        }
        var user = await db.Users.FirstOrDefaultAsync(x => x.NormalizedUserName == User.Identity.Name);
        dliib.Author = user;
        db.Dliibs.Add(dliib);
        await db.SaveChangesAsync();

        return CreatedAtAction("GetDliib", new { id = dliib.Id }, dliib);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteDliib(int id)
    {
        var dliib = await db.Dliibs.FindAsync(id);
        if (dliib == null)
        {
            return NotFound();
        }

        if (User.Identity == null)
        {
            return Unauthorized();
        }
        var user = await db.Users.FirstOrDefaultAsync(x => x.NormalizedUserName == User.Identity.Name);
        if (dliib.Author != user)
        {
            return Unauthorized();
        }

        db.Dliibs.Remove(dliib);
        await db.SaveChangesAsync();

        return NoContent();
    }

    private bool DliibExists(int id)
    {
        return db.Dliibs.Any(e => e.Id == id);
    }
}