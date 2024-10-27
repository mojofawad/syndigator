using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Syndigator.Feeds.Controllers;

[ApiController]
[Route("[controller]")]
public class FeedsController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<FeedsController> _logger;

    public FeedsController(ILogger<FeedsController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetFeeds")]
    public IEnumerable<FeedsViewModel> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new FeedsViewModel
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}

public class FeedsViewModel
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}