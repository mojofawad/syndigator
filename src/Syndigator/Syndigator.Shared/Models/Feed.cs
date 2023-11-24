namespace Syndigator.Shared.Models;

public class Feed
{
    public string Title { get; set; }
    public string Description { get; set; }
    public List<Article> Articles { get; set; }
}