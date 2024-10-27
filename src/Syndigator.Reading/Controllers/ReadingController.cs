using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Syndigator.Reading.Controllers;

[ApiController]
[Route("[controller]")]
public class ReadingController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<ReadingController> _logger;

    public ReadingController(ILogger<ReadingController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetReadings")]
    public IEnumerable<ReadingViewModel> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new ReadingViewModel
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}

public class ReadingViewModel
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}