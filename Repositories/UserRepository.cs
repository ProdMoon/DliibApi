using DliibApi.Data;
using Microsoft.EntityFrameworkCore;

namespace DliibApi.Repositories;

public class UserRepository(AppDbContext db)
{
    public async Task<DliibUser?> GetUserById(string userId)
    {
        return await db.Users.FindAsync(userId);
    }

    public async Task<DliibUser?> GetUserByName(string userName)
    {
        return await db.Users.FirstOrDefaultAsync(x => x.NormalizedUserName == userName);
    }
}