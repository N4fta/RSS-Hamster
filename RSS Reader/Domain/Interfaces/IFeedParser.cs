using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IFeedParser
    {
        // Async due to nature of the NuGet package I use to help parse feeds
        public Task<ParsedFeed?> ParseFeedAsync(Feed feed, IFeedItemParser itemParser);
    }
}
