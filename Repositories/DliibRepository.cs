using AutoMapper;
using DliibApi.Data;
using DliibApi.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DliibApi.Repositories;

public class DliibRepository(AppDbContext db, IMapper mapper)
{
    public async Task<IEnumerable<DliibDto>> GetAllDliibDtos()
    {
        var dliibs = await db.Dliibs
            .Include(x => x.Author)
            .Include(x => x.Likes)
            .Include(x => x.Dislikes)
            .OrderByDescending(x => x.Id)
            .ToListAsync();
        
        return mapper.Map<List<DliibDto>>(dliibs);
    }

    public async Task<IEnumerable<Dliib>> GetAllDliibs()
    {
        return await db.Dliibs
            .Include(x => x.Author)
            .Include(x => x.Likes)
            .Include(x => x.Dislikes)
            .OrderByDescending(x => x.Id)
            .ToListAsync();
    }

    public async Task<DliibDto> GetDliibDto(int id)
    {
        var dliib = await db.Dliibs.FindAsync(id);

        return mapper.Map<DliibDto>(dliib);
    }

    public async Task<Dliib?> GetDliib(int id)
    {
        return await db.Dliibs.FindAsync(id);
    }

    public async Task<IEnumerable<DliibDto>> GetUserDliibDtos(string userId)
    {
        var dliibs = await db.Dliibs
            .Include(x => x.Author)
            .Include(x => x.Likes)
            .Include(x => x.Dislikes)
            .Where(x => x.Author != null && x.Author.Id == userId)
            .OrderByDescending(x => x.Id)
            .ToListAsync();

        return mapper.Map<List<DliibDto>>(dliibs);
    }

    public async Task<int> Create(Dliib dliib)
    {
        db.Dliibs.Add(dliib);
        
        return await db.SaveChangesAsync();
    }

    public async Task<DliibDto> Update(DliibDto dliibDto)
    {
        var dliib = await db.Dliibs.FindAsync(dliibDto.Id);
        if (dliib == null)
        {
            return null;
        }

        mapper.Map(dliibDto, dliib);
        await db.SaveChangesAsync();

        return mapper.Map<DliibDto>(dliib);
    }

    public async Task<bool> Delete(int id)
    {
        var dliib = await db.Dliibs.FindAsync(id);
        if (dliib == null)
        {
            return false;
        }

        db.Dliibs.Remove(dliib);
        var result = await db.SaveChangesAsync();

        return result > 0;
    }
}