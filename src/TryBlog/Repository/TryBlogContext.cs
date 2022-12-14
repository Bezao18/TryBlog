using Microsoft.EntityFrameworkCore;
using projeto_final.Models;

namespace projeto_final.Repository;

public class TryBlogContext : DbContext, ITryBlogContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users { get; set; }
    public TryBlogContext(DbContextOptions<TryBlogContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(@"
                Server=127.0.0.1;
                Database=TryBlog;
                User=SA;
                Password=Senha123$;
                TrustServerCertificate=True;
            ").UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);;
        }
    }
}
