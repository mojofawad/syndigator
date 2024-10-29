using Moq;
using Syndigator.Feeds.Application.Commands.CreateFeed;
using Syndigator.Feeds.Domain.Entities;
using Syndigator.Feeds.Domain.Interfaces;

namespace Syndigator.Feeds.UnitTests.Application.Commands.CreateFeed;

public class CreateFeedCommandHandlerTests
{
    private readonly CreateFeedCommandHandler _sut;
    private readonly Mock<IFeedRepository> _feedRepository;
    
    public CreateFeedCommandHandlerTests()
    {
        _feedRepository = new Mock<IFeedRepository>();
        _sut = new CreateFeedCommandHandler(_feedRepository.Object);
    }

    [Fact]
    public async Task Handle_WhenUrlIsValid_ShouldCreateFeed()
    {
        // Arrange
        var url = "https://example.com/feed.xml";
        var command = new CreateFeedCommand(url);

        _feedRepository.Setup(r => r.AddAsync(It.IsAny<Feed>()))
            .ReturnsAsync((Feed feed) => feed);
        
        // Act
        var result = await _sut.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(url, result.Url);
        _feedRepository.Verify(r => r.AddAsync(It.Is<Feed>(
            f => f.Url == url)), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenRepositoryThrows_ShouldPropagateException()
    {
        // Arrange
        var command = new CreateFeedCommand("https://example.com/feed.xml");
        _feedRepository.Setup(r => r.AddAsync(It.IsAny<Feed>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => 
            _sut.Handle(command, CancellationToken.None));
    }
}