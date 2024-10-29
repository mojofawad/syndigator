using Syndigator.Feeds.Domain.Entities;
using Syndigator.Feeds.Domain.Interfaces;

namespace Syndigator.Feeds.Application.Commands.CreateFeed;

public class CreateFeedCommandHandler
{
    private readonly IFeedRepository _feedRepository;
    
    public CreateFeedCommandHandler(IFeedRepository feedRepository)
    {
        _feedRepository = feedRepository;
    }
    
    public async Task<Feed> Handle(CreateFeedCommand command, CancellationToken cancellationToken)
    {
        var feed = Feed.Create(command.Url);
        var savedFeed = await _feedRepository.AddAsync(feed);
        return savedFeed;
    }
}