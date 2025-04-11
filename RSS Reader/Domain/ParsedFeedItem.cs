using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{

    public class ParsedFeedItem
    {
        public string Id { get; }
        public string Title { get; }
        public DateTime? Published { get; }
        public string Link { get; }
        public string Author { get; }
        public string MainBody { get; }
        public object? Media { get; }

        public ParsedFeedItem(string id, string title, DateTime? published, string link, string author, string mainBody, object? media = null)
        {
            Id = id;
            Title = title;
            Published = published;
            Link = link;
            Author = author;
            MainBody = mainBody;
            Media = media;
        }
    }
}
