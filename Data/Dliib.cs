using Microsoft.EntityFrameworkCore;

namespace DliibApi.Data;

[Index(nameof(CreatedAt))]
public class Dliib
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(9);
    public DliibUser? Author { get; set; }
    public List<DliibLike> Likes { get; set; } = [];
    public List<DliibDislike> Dislikes { get; set; } = [];
}