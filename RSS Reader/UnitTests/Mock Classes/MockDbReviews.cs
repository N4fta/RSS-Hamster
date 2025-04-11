using Data;
using Data.DTOs;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Mock_Classes
{
    internal class MockDbReviews : IDbConnectionReviews
    {
        private Dictionary<int, ReviewDTO> reviewRepo = new();

        public MockDbReviews(Dictionary<int, ReviewDTO> reviewRepo)
        {
            this.reviewRepo = reviewRepo;
        }

        public DBResult InsertReview(ReviewDTO reviewDTO)
        {
            reviewRepo.Add(reviewDTO.Id, reviewDTO);
            return new DBResult(true);
        }

        public List<ReviewDTO> LoadReviews(int count = 100, OrderByReviews? orderBy = null, int userID = 0, int feedID = 0, bool active = true)
        {
            return reviewRepo.Values.ToList();
        }

        public DBResult UpdateReview(ReviewDTO reviewDTO)
        {
            reviewRepo[reviewDTO.Id] = reviewDTO;
            return new DBResult(true);
        }
        public DBResult DeleteReview(int reviewID)
        {
            reviewRepo.Remove(reviewID);
            return new DBResult(true);
        }
    }
}
