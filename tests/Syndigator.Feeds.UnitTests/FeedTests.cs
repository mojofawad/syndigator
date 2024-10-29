using mojofawad.Shared.Domain.Exceptions;
using Syndigator.Feeds.Domain.Entities;

namespace Syndigator.Feeds.UnitTests;

public class FeedTests
{
    [Fact]
    public void Create_WhenUrlIsValid_ShouldCreateFeed()
    {
        // Arrange
        var url = "http://feeds.bbci.co.uk/news/rss.xml";
        
        // Act
        var feed = Feed.Create(url);
        
        // Assert
        Assert.NotNull(feed);
        Assert.Equal(url, feed.Url);
        Assert.False(feed.IsWorking);
        Assert.Null(feed.LastChecked);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    [InlineData("not-a-url")]
    [InlineData("ftp://invalid-scheme.com")]
    public void Create_WhenUrlIsInvalid_ShouldThrowDomainValidationException(string invalidUrl)
    {
        Assert.Throws<DomainValidationException>(() => Feed.Create(invalidUrl));
    }

    [Fact]
    public void UpdateStatus_WhenFeedIsWorking_ShouldUpdateStatusAndTimestamp()
    {
        // Arrange 
        var feed = Feed.Create("http://feeds.bbci.co.uk/news/rss.xml");
        var timestamp = DateTime.UtcNow;
        
        // Act
        feed.UpdateStatus(isWorking: true, timestamp);
        
        // Assert
        Assert.True(feed.IsWorking);
        Assert.Equal(timestamp, feed.LastChecked);
    }

    [Fact]
    public void UpdateStatus_WhenFeedIsNotWorking_ShouldUpdateStatusAndTimestamp()
    {
        // Arrange
        var feed = Feed.Create("https://example.com/feed.xml");
        var timestamp = DateTime.UtcNow;
        
        // Act
        feed.UpdateStatus(isWorking: false, timestamp);
        
        // Assert
        Assert.False(feed.IsWorking);
        Assert.Equal(timestamp, feed.LastChecked);
    }
    
    [Fact]
    public void UpdateStatus_WhenTimestampIsUtc_ShouldAcceptTimestamp()
    {
        // Arrange
        var feed = Feed.Create("https://example.com/feed.xml");
        var timestamp = DateTime.UtcNow;
        
        // Act
        feed.UpdateStatus(isWorking: true, timestamp);
        
        // Assert
        Assert.Equal(timestamp, feed.LastChecked);
    }
    
    [Fact]
    public void UpdateStatus_WhenTimestampIsNotUtc_ShouldThrowDomainValidationException()
    {
        // Arrange
        var feed = Feed.Create("https://example.com/feed.xml");
        var timestamp = DateTime.Now;
        
        // Act & Assert
        var exception = Assert.Throws<DomainValidationException>(() =>
            feed.UpdateStatus(isWorking: true, timestamp));
        
        Assert.Equal("Timestamp must be in UTC", exception.Message);
    }
}