namespace Syndigator.Web;

public class SyndigatorApiClient(HttpClient httpClient)
{
    public async Task<Feed[]> GetFeedAsync(int maxItems = 10, CancellationToken cancellationToken = default)
    {
        List<Feed>? feeds = null;

        await foreach (var feed in httpClient.GetFromJsonAsAsyncEnumerable<Feed>("/feed", cancellationToken))
        {
            if (feeds?.Count >= maxItems)
            {
                break;
            }
            if (feed is not null)
            {
                feeds ??= [];
                feeds.Add(feed);
            }
        }

        return feeds?.ToArray() ?? [];
    }
}

public record Feed(int FeedId, string Name, string Url, FeedType Type, List<Item> Items);

public record Item(int ItemId, string Title, string Url, string Content, DateTimeOffset Published);

public enum FeedType
{
    Rss,
    Atom,
    Rdf,
    Unknown
}