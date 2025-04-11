using CodeHollow.FeedReader;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.FeedParsers.FeedItemParsers
{
    public class DefaultFeedItemParser : IFeedItemParser
    {
        public List<ParsedFeedItem> ParseFeedItems(IList<FeedItem> feedItems)
        {
            List<ParsedFeedItem> parsedFeedItems = new();

            foreach (var item in feedItems)
            {
                // All content goes here
                string mainBody = item.Description;


                ParsedFeedItem parsedItem = new(item.Id, item.Title, item.PublishingDate, item.Link, item.Author, mainBody);

                parsedFeedItems.Add(parsedItem);
            }

            return parsedFeedItems;
        }

        public override string ToString()
        {
            return "DefaultFeedItemParser";
        }
    }
}
