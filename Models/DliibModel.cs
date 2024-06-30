namespace DliibApi.Models;

public class DliibModel
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public DateTime CreatedAt { get; set; }
    public string AuthorNickName { get; set; } = null!;
}
