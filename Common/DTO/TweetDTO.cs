using Common.DTO;
using Domain.Entities;
using System.Text.Json.Serialization;

public class TweetDTO
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }

    //// TwitterUser bilgileri
    //[JsonIgnore]
    //public TwitterUser TwitterUser { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }

}
