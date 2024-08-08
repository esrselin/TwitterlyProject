using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
    public class Tweet
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public int UserId { get; set; }
        public virtual TwitterUser TwitterUser { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
