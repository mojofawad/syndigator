using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Syndigator.UserProfiles.Controllers;

[ApiController]
[Route("syndigator/[controller]")]
public class UserProfilesController(ILogger<UserProfilesController> logger) : ControllerBase
{
    private readonly ILogger<UserProfilesController> _logger = logger;

    [HttpGet(Name = "GetUserProfiles")]
    public List<UserProfile> Get()
    {
        return
        [
            new UserProfile("Alice", "alice@example.com"),
            new UserProfile("Bob", "bob@example.com"),
            new UserProfile("Charlie", "charlie@example.com")
        ];
    }
}

public class UserProfile(string name, string email)
{
    public string Name { get; set; } = name;
    public string Email { get; set; } = email;
}