using CodeHollow.FeedReader;

namespace Domain.Interfaces
{
    public interface IFeedItemParser
    {
        // Async due to nature of the NuGet package I use to help parse feeds
        public List<ParsedFeedItem> ParseFeedItems(IList<FeedItem> feedItems);
    }
}