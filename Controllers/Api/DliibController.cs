using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DliibApi.Data;
using Microsoft.AspNetCore.Authorization;

namespace DliibApi.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class DliibController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Dliib>>> GetDliibs()
    {
        return await db.Dliibs
            .Include(x => x.Author)
            .OrderByDescending(x => x.Id)
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
        var user = await db.Users.FirstOrDefaultAsync(x => x.NormalizedUserName == User.Identity.Name);
        dliib.Author = user;
        db.Dliibs.Add(dliib);
        await db.SaveChangesAsync();

        return CreatedAtAction("GetDliib", new { id = dliib.Id }, dliib);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDliib(int id)
    {
        var dliib = await db.Dliibs.FindAsync(id);
        if (dliib == null)
        {
            return NotFound();
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