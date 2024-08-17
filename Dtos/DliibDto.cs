namespace DliibApi.Dtos;

public class DliibDto
{
    public int Id { get; set; }
    public List<string> Contents { get; set; } = null!;
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public bool IsLiked { get; set; }
    public bool IsDisliked { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(9);
    public string? AuthorNickName { get; set; }
}
