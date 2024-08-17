namespace DliibApi.Data;

public class DliibContent
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public int Order { get; set; }
    public Dliib Dliib { get; set; } = null!;
}