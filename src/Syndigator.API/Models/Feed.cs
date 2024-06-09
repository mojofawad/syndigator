public class Feed
{
    public int FeedId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    
    public FeedType Type { get; }
    
    public List<Item> Items { get; set; }
}