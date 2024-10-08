using Microsoft.EntityFrameworkCore;

namespace DliibApi.Data;

[Index(nameof(CreatedAt))]
public class Dliib
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(9);
    public DliibUser? Author { get; set; }
    public List<DliibContent> Contents { get; set; } = [];
    public List<DliibLike> Likes { get; set; } = [];
    public List<DliibDislike> Dislikes { get; set; } = [];
}