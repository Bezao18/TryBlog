namespace projeto_final.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class Post
{
        public Guid PostId { get; set; }
        
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public string? Content { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset LastModified { get; set; }

}
