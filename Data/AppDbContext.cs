using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DliibApi.Data;

public class AppDbContext : IdentityDbContext<DliibUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Dliib> Dliibs { get; set; }
}
