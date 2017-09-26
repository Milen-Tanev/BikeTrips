using System;
using System.ComponentModel.DataAnnotations;

namespace BikeTrips.Data.Models
{
    public class Comment
    {
        private string content;
        private User author;

        public Comment(string content, User author, int localTimeOffsetMinutes)
        {
            this.Content = content;
            this.Author = author;
            this.UtcTime = DateTime.UtcNow;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public virtual User Author { get; set; }

        [Required]
        public int LocalTimeOffsetMinutes { get; protected set; }

        [Required]
        public DateTime UtcTime { get; private set; }
    }
}
