using Microsoft.EntityFrameworkCore;
using projeto_final.Models;

namespace projeto_final.Repository

{
    public interface ITryBlogContext
    {
        DbSet<Post> Posts { get; set; }
        DbSet<User> Users { get; set; }
        int SaveChanges();
    }
}