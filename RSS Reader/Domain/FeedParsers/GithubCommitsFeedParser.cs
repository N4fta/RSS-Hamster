using CodeHollow.FeedReader;
using Domain.FeedParsers.FeedItemParsers;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.FeedParsers
{
    public class GithubCommitsFeedParser : IFeedParser
    {
        public async Task<ParsedFeed?> ParseFeedAsync(Feed feedDB, IFeedItemParser? itemParser = null)
        {
            var feed = await FeedReader.ReadAsync(feedDB.Source);

            if (feed.Items.Count == 0) { return null; }

            if (itemParser == null) itemParser = new DefaultFeedItemParser();

            DateTime lastUpdatedDate;
            if (feed.LastUpdatedDate.HasValue) lastUpdatedDate = feed.LastUpdatedDate.Value;
            else lastUpdatedDate = DateTime.MinValue;

            var parsedFeedItems = itemParser.ParseFeedItems(feed.Items);

            // Title requires manipulation
            string title = feed.Title;

            ParsedFeed parsedFeed = new(title, lastUpdatedDate, feed.Link, feed.Type, parsedFeedItems);

            return parsedFeed;
        }

        public override string ToString()
        {
            return "GithubCommitsFeedParser";
        }
    }
}
