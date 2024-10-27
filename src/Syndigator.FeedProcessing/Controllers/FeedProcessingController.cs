using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Syndigator.FeedProcessing.Controllers;

[ApiController]
[Route("syndigator/[controller]")]
public class FeedProcessingController(ILogger<FeedProcessingController> logger) : ControllerBase
{
    private readonly ILogger<FeedProcessingController> _logger = logger;

    [HttpGet(Name = "GetFeedStatus")]
    public IEnumerable<FeedStatus> Get()
    {
        return
        [
            new FeedStatus("Feed1", "The first feed.", "Active"),
            new FeedStatus("Feed2", "The second feed.", "Inactive"),
            new FeedStatus("Feed3", "The third feed.", "Active")
        ];
    }
}

public class FeedStatus(string name, string description, string status)
{
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public string Status { get; set; } = status;
}