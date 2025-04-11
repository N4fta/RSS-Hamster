using Domain.FeedParsers;
using Domain.FeedParsers.FeedItemParsers;
using Domain.Interfaces;

namespace Domain
{
    public class Feed
    {
        public int Id { get; }
        public string Name { get; }
        public string Source { get; }
        public List<string> Categories { get; }
        public int NumberOfCategories { get { return Categories.Count; } }
        public IFeedParser FeedParser { get; } = new DefaultFeedParser();
        public string FeedParserToString { get { return FeedParser.GetType().ToString().Split('.')[2]; } }
        public IFeedItemParser FeedItemParser { get; } = new DefaultFeedItemParser();
        public string FeedItemParserToString { get { return FeedItemParser.GetType().ToString().Split('.')[3]; } }
        public List<Review> Reviews { get; }

        public Feed(string name, string source, List<Review> reviews, List<string> categories, IFeedParser? feedParse = null, IFeedItemParser? feedItemParser = null)
        {
            Id = 0;
            Name = name;
            Source = source;
            Reviews = reviews;
            Categories = categories;

            if (feedParse != null) FeedParser = feedParse;
            if (feedItemParser != null) FeedItemParser = feedItemParser;
        }
        public Feed(int id, string name, string source, List<Review> reviews, List<string> categories, IFeedParser? feedParse = null, IFeedItemParser? feedItemParser = null)
        {
            Id = id;
            Name = name;
            Source = source;
            Reviews = reviews;
            Categories = categories;

            if (feedParse != null) FeedParser = feedParse;
            if (feedItemParser != null) FeedItemParser = feedItemParser;
        }

        public async Task<ParsedFeed> ParseFeedAsync()
        {
            ParsedFeed parsedFeed = await FeedParser.ParseFeedAsync(this, FeedItemParser);
            return parsedFeed;
        }

        public override string ToString()
        {
            return Name;
        }

        
    }

}
