using DliibApi.Data;
using Microsoft.EntityFrameworkCore;

namespace DliibApi.Repositories;

public class DliibLikeRepository(AppDbContext db)
{
    public async Task<int> Like(int dliibId, string userName)
    {
        var dliib = await db.Dliibs.FirstOrDefaultAsync(x => x.Id == dliibId);
        var user = await db.Users.FirstOrDefaultAsync(x => x.NormalizedUserName == userName);
        if (dliib == null || user == null)
        {
            return 0;
        }

        db.DliibLikes.Add(new DliibLike { Dliib = dliib, User = user });
        
        return await db.SaveChangesAsync();
    }

    public async Task<int> CancelLike(int id)
    {
        var like = await db.DliibLikes.FindAsync(id);
        if (like == null)
        {
            return 0;
        }

        db.DliibLikes.Remove(like);

        return await db.SaveChangesAsync();
    }

    public async Task<int> Dislike(int dliibId, string userName)
    {
        var dliib = await db.Dliibs.FirstOrDefaultAsync(x => x.Id == dliibId);
        var user = await db.Users.FirstOrDefaultAsync(x => x.NormalizedUserName == userName);
        if (dliib == null || user == null)
        {
            return 0;
        }

        db.DliibDislikes.Add(new DliibDislike { Dliib = dliib, User = user });
        
        return await db.SaveChangesAsync();
    }

    public async Task<int> CancelDislike(int id)
    {
        var dislike = await db.DliibDislikes.FindAsync(id);
        if (dislike == null)
        {
            return 0;
        }

        db.DliibDislikes.Remove(dislike);
        
        return await db.SaveChangesAsync();
    }

    public async Task<int?> GetLikeId(int dliibId, string userName)
    {
        return await db.DliibLikes
            .Where(x => x.Dliib.Id == dliibId && x.User.NormalizedUserName == userName)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
    }

    public async Task<int?> GetDislikeId(int dliibId, string userName)
    {
        return await db.DliibDislikes
            .Where(x => x.Dliib.Id == dliibId && x.User.NormalizedUserName == userName)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
    }
}