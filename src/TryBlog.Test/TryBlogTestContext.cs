using Microsoft.EntityFrameworkCore;
using projeto_final.Models;
using projeto_final.Repository;

namespace projeto_final.Test;

public class TryBlogTestContext : DbContext, ITryBlogContext
{
    public TryBlogTestContext(DbContextOptions<TryBlogTestContext> options)
            : base(options)
    { }
    public virtual DbSet<Post> Posts { get; set; }
    public virtual DbSet<User> Users { get; set; }
}
