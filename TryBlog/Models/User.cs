namespace projeto_final;

public class User
{
        public Guid UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }

        public ICollection<Post>? Posts { get; set; }
}
