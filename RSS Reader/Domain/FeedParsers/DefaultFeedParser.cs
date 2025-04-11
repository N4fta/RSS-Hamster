using CodeHollow.FeedReader;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.FeedParsers
{
    public class DefaultFeedParser : IFeedParser
    {
        public async Task<ParsedFeed?> ParseFeedAsync(Feed feedDB, IFeedItemParser itemParser)
        {
            var feed = await FeedReader.ReadAsync(feedDB.Source);

            if (feed.Items.Count == 0) { return null; }

            DateTime? lastUpdatedDate;
            if (feed.LastUpdatedDate.HasValue) lastUpdatedDate = feed.LastUpdatedDate.Value;
            // If the field is missing check the publish date of most recent item
            else if (feed.Items[0].PublishingDate.HasValue) lastUpdatedDate = feed.Items[0].PublishingDate.Value;
            else lastUpdatedDate = DateTime.MinValue;

            var parsedFeedItems = itemParser.ParseFeedItems(feed.Items);

            ParsedFeed parsedFeed = new(feed.Title, lastUpdatedDate.Value, feed.Link, feed.Type, parsedFeedItems);

            return parsedFeed;
        }

        public override string ToString()
        {
            return "DefaultFeedParser";
        }
    }
}
