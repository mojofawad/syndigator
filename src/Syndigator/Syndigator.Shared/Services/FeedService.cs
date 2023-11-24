using System.ServiceModel.Syndication;
using System.Xml;


namespace Syndigator.Shared.Services;

public class FeedService
{
    public List<FeedItem> ParseFeed(string url)
    {
        using var reader = XmlReader.Create(url);
        var feed = SyndicationFeed.Load(reader);
        var feedItems = new List<FeedItem>();

        foreach (var item in feed.Items)
        {
            var feedItem = new FeedItem
            {
                Title = item.Title.Text,
                Summary = item.Summary?.Text,
                PublishDate = item.PublishDate.DateTime,
                Link = item.Links.Count > 0 ? item.Links[0].Uri.ToString() : string.Empty
            };
            feedItems.Add(feedItem);
        }

        return feedItems;
    }
}

public class FeedItem
{
    public string Title { get; set; }
    public string Summary { get; set; }
    public DateTime PublishDate { get; set; }
    public string Link { get; set; }
}