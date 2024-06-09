public class Item
{
    public int ItemId { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public string Content { get; set; }
    public DateTime Published { get; set; }
    
    public Feed Feed { get; set; }
    public int FeedId { get; set; }
}