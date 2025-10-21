namespace API.Models;

public class AppLog
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public required string Timestamp { get; set; }
}