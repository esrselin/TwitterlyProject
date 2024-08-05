using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities
{
    public class Tweet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [ForeignKey("TwitterUser")]
        public string UserId { get; set; }
        public TwitterUser TwitterUser { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
