using Domain.FeedParsers;
using Domain.FeedParsers.FeedItemParsers;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ParserService
    {
        private List<IFeedParser> parsers;
        private List<IFeedItemParser> itemParsers;

        public ParserService()
        {
            // Parser IDs are their index in the list
            parsers = new()
            {
                new DefaultFeedParser(),
                new GithubCommitsFeedParser()
            };

            itemParsers = new()
            {
                new DefaultFeedItemParser(),
                new YoutubeChannelItemParser()
            };
        }

        public int GetParserID(IFeedParser feedParser)
        {
            return parsers.FindIndex(p => p.GetType() == feedParser.GetType());
        }

        public int GetItemParserID(IFeedItemParser feedItemParser)
        {
            return itemParsers.FindIndex(p => p.GetType() == feedItemParser.GetType());
        }

        public IFeedParser? GetParser(int id)
        {
            try
            {
                return parsers[id];
            }
            catch
            {
                return null;
            }
        }

        public IFeedItemParser? GetItemParser(int id)
        {
            try
            {
                return itemParsers[id];
            }
            catch
            {
                return null;
            }
        }

        public List<IFeedParser> GetAllParsers()
        {
            return parsers;
        }

        public List<IFeedItemParser> GetAllItemParsers()
        {
            return itemParsers;
        }
    }
}
