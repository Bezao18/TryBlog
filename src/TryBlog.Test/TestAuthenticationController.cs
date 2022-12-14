using Microsoft.AspNetCore.Mvc.Testing;
using projeto_final.Models;


namespace TryBlog.Test;

public class TestAuthenticationController : IClassFixture<WebApplicationFactory<Program>>
{
   private readonly WebApplicationFactory<Program> _factory;

   public TestClientController(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
}