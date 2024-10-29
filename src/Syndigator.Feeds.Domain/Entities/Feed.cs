using mojofawad.Shared.Domain.Entities;
using mojofawad.Shared.Domain.Exceptions;

namespace Syndigator.Feeds.Domain.Entities;

public class Feed : Entity
{
    public string Url { get; private set; }
    public bool IsWorking { get; private set; }
    public DateTime? LastChecked { get; private set; }
    
    private Feed(string url)
    {
        Url = url;
        IsWorking = false;
    }

    public static Feed Create(string url)
    {
        ValidateUrl(url);
        return new Feed(url);
    }

    private static void ValidateUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new DomainValidationException("Url cannot be empty");
        }

        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
        {
            throw new DomainValidationException("Url is not a valid URL");
        }
        
        if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
        {
            throw new DomainValidationException("Url must be HTTP or HTTPS");
        }
    }

    public void UpdateStatus(bool isWorking, DateTime timestamp)
    {
        if (!timestamp.Kind.Equals(DateTimeKind.Utc))
        {
            throw new DomainValidationException("Timestamp must be in UTC");
        }
        
        IsWorking = isWorking;
        LastChecked = timestamp;
    }
}