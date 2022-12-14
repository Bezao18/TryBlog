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

    [Fact(DisplayName = "POST /signup deve retornar um token")]
    public async Task SuccesfulSignUp()
    {
        var user = new User {   Username = "aaaaaaaaaa",
            Email = "aaaa@gmail.com",
            Password ="123456789"
            };
        var httpResponse = await client.PostAsJsonAsync("/signup", user);
        var token = await httpResponse.Content.ReadAsStringAsync();
        token.Should().BeOfType(typeof(string));
        httpResponse.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact(DisplayName = "POST /signup deve retornar status 400 caso a senha ou usu??rio n??o sejam informados")]
    public async Task FailedSignUp_RequiredFields()
    {
        var user = new User {   Username = "aaaaaaaaaa",
            Email = "aaaa@gmail.com",
            };
        var httpResponse = await client.PostAsJsonAsync("/signup", user);
        var content =  await httpResponse.Content.ReadAsStringAsync();
        content.Should().Be("Username and password are required");
        httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    
    [Fact(DisplayName = "POST /signup deve retornar status 400 caso o usu??rio j?? exista")]
    public async Task FailedSignUp_UserExists()
    {
        var user = new User {   Username = "Usu??rio1",
            Email = "teste@gmail.com",
            Password = "123456789",
            };
        var httpResponse = await client.PostAsJsonAsync("/signup", user);
        var content =  await httpResponse.Content.ReadAsStringAsync();
        content.Should().Be("User already exists");
        httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact(DisplayName = "POST /login deve retornar status 404 caso o usu??rio n??o exista")]
    public async Task FailedLogin_UserNotFound()
    {
        var user = new User {   Password = "aaaaaaaaaa",
            Email = "aaaaaaaaa@gmail.com",
            };
        var httpResponse = await client.PostAsJsonAsync("/login", user);
        var content =  await httpResponse.Content.ReadAsStringAsync();
        content.Should().Be("User not Found");
        httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact(DisplayName = "POST /login deve retornar status 400 caso a senha esteja incorreta")]
    public async Task FailedLogin_InvalidCredentials()
    {
        var user = new User {Email = "teste@gmail.com",
            Password = "1234567890",
            };
        var httpResponse = await client.PostAsJsonAsync("/login", user);
        var content =  await httpResponse.Content.ReadAsStringAsync();
        content.Should().Be("Invalid credentials");
        httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact(DisplayName = "POST /login deve retornar status 200")]
    public async Task SuccesfulLogin()
    {
        var user = new User {Email = "teste@gmail.com",
            Password = "123456789",
            };
        var httpResponse = await client.PostAsJsonAsync("/login", user);
        var token =  await httpResponse.Content.ReadAsStringAsync();
        token.Should().BeOfType(typeof(string));
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
