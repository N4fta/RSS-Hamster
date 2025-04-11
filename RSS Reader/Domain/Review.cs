using Data;
using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Review
    {
        public int Id { get; }
        public string Title { get; set;  }
        public string MainBody { get; set;  }
        public int Likes { get; set; }

        public Review(string title, string mainBody)
        {
            Id = 0;
            Title = title;
            MainBody = mainBody;
            Likes = 0;
        }
        public Review(int id, string title, string mainBody, int likes)
        {
            Id = id;
            Title = title;
            MainBody = mainBody;
            Likes = likes;
        }

    }
}
