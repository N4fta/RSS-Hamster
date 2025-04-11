using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Interfaces
{
    public interface IDbConnectionReviews
    {
        DBResult InsertReview(ReviewDTO reviewDTO);

        List<ReviewDTO> LoadReviews(int count = 100, OrderByReviews? orderBy = null, int userID = 0, int feedID = 0, bool active = true);

        DBResult UpdateReview(ReviewDTO reviewDTO);

        DBResult DeleteReview(int reviewID);
    }
}
