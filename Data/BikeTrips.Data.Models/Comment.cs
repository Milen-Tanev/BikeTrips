using System;

namespace BikeTrips.Data.Models
{
    public class Comment
    {
        public Comment(string content, User user)
        {
            this.Content = content;
            this.CommentTime = DateTime.UtcNow;
        }

        public string Content { get; set; }
        public virtual User user { get; set; }
        public DateTime CommentTime { get; set; }
    }
}
