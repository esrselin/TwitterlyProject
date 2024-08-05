using Domain.Entities;

public class TweetDTO
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string UserId { get; set; }
    public DateTime CreatedAt { get; set; }

    // TwitterUser bilgileri
    public TwitterUser TwitterUser { get; set; }

}
