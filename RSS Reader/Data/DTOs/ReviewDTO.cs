using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string MainBody { get; set; }
        public int FeedID { get; set; }
        public int UserID { get; set; }
        public int Likes { get; set; }
    }
}
