using CodeHollow.FeedReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ParsedFeed
    {
        public string Title { get; }
        public DateTime LastUpdated{ get; }
        public string Link { get; }
        private FeedType _type { get; }
        public IReadOnlyList<ParsedFeedItem> Items { get; }


        public ParsedFeed(string title, DateTime lastUpdated, string link, FeedType type, List<ParsedFeedItem> items)
        {
            Title = title;
            LastUpdated = lastUpdated;
            Link = link;
            _type = type;
            Items = items;
        }
    }
}
