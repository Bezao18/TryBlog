using projeto_final.Models;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TryBlog.Test
{
  public static TryBlogContext GetContextInstanceForTests(string inMemoryDbName)
        {
            var contextOptions = new DbContextOptionsBuilder<TryBlogContext>()
                .UseInMemoryDatabase(inMemoryDbName)
                .Options;
            var context = new TryBlogContext(contextOptions);
            context.User.AddRange(
                GetChannelListForTests()
            );
            context.Videos.AddRange(
                GetVideoListForTests()
            );
            context.Users.Add(new User { UserId = 1, Username = "Test", Email = "Test" });
            context.Comments.AddRange(
                GetCommentListForTests()
            );
            context.SaveChanges();
            return context;
        }
}