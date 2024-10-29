using Syndigator.Feeds.Domain.Entities;

namespace Syndigator.Feeds.Domain.Interfaces;

public interface IFeedRepository
{
    Task<Feed> AddAsync(Feed feed);
}