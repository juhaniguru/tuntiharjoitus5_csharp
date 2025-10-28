namespace API.Models;

public class AppLog
{
    public long Id { get; set; }
    public required string UserName { get; set; }
    public required string Timestamp { get; set; }
}