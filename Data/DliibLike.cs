namespace DliibApi.Data;

public class DliibLike
{
    public int Id { get; set; }
    public DliibUser User { get; set; } = null!;
    public Dliib Dliib { get; set; } = null!;
}