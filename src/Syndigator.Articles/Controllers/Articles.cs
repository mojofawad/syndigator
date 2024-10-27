using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Syndigator.Articles.Controllers;

[ApiController]
[Route("syndigator/[controller]")]
public class Articles(ILogger<Articles> logger) : ControllerBase
{
    private readonly ILogger<Articles> _logger = logger;

    [HttpGet(Name = "GetArticles")]
    public IEnumerable<ArticleViewModel> Get()
    {
        return new List<ArticleViewModel>
        {
            new ArticleViewModel(
                "Hello, world!",
                "Welcome to Syndigator!",
                "Alice",
                DateTime.Now,
                new string[] { "hello", "world" },
                new string[] { "welcome", "syndigator" }
            ),
            new ArticleViewModel(
                "Goodbye, world!",
                "Farewell from Syndigator!",
                "Bob",
                DateTime.Now,
                new string[] { "goodbye", "world" },
                new string[] { "farewell", "syndigator" }
            )
        };
    }
}

public class ArticleViewModel(
    string title,
    string content,
    string author,
    DateTime publishedDate,
    string[] tags,
    string[] categories
    )
{
    public string Title { get; set; } = title;
    public string Content { get; set; } = content;
    public string Author { get; set; } = author;
    public DateTime PublishedDate { get; set; } = publishedDate;
    public string[] Tags { get; set; } = tags;
    public string[] Categories { get; set; } = categories;
}