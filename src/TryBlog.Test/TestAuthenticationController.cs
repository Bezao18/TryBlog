using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using projeto_final.Repository;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using projeto_final.Models;
using System.Text.Json.Nodes;
using System.Net.Http.Json;

namespace TryBlog.Test;

public class TestAuthenticationController : IClassFixture<WebApplicationFactory<Program>>
{
    public HttpClient client;   

    public TestAuthenticationController(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TryBlogContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }
                services.AddDbContext<TryBlogTestContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryTest");
                });
                services.AddScoped<ITryBlogContext, TryBlogTestContext>();
                services.AddScoped<ITryBlogRepository, TryBlogRepository>();
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                using (var appContext = scope.ServiceProvider.GetRequiredService<TryBlogTestContext>())
                {
                    appContext.Database.EnsureCreated();
                    appContext.Database.EnsureDeleted();
                    appContext.Database.EnsureCreated();
                    appContext.Users.AddRange(
                        MockDB.GetUserListForTests()
                    );
                    appContext.Posts.AddRange(
                        MockDB.GetPostListForTests()
                    );
                    appContext.SaveChanges();
                }
            });
        }).CreateClient();
    }

    // [Theory(DisplayName = "POST /signup deve retornar um token")]
    // [MemberData(nameof(ShouldReturnAVideoListData))]
    [Fact(DisplayName = "POST /signup deve retornar um token")]
    public async Task ShouldReturnAToken()
    {
        var httpResponse = await client.PostAsync("signup", new StringContent(JsonSerializer.Serialize( new User{Email="aaa@aaaa.com", Password="123456789", Username="Usuário3"})));
        var token = await httpResponse.Content.ReadAsStringAsync();
        token.Should().BeOfType(typeof(string));
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
