using CodeHollow.FeedReader;
using Data;
using Data.DatabaseConnections;
using Data.DTOs;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ReviewService
    {
        private IDbConnectionReviews _connectionReviews;

        public ReviewService()
        {
            _connectionReviews = new DbConnectionReviews();
        }
        public ReviewService(IDbConnectionReviews dbConnectionReviews)
        {
            _connectionReviews = dbConnectionReviews;
        }

        public DBResult Add(Review review, int feedID, int userID)
        {
            return _connectionReviews.InsertReview(ConvertToDTO(review, feedID, userID));
        }

        public DBResult Update(Review review)
        {
            return _connectionReviews.UpdateReview(ConvertToDTO(review));
        }

        public DBResult Update(List<Review> reviews)
        {
            int rowsAffected = 0;
            foreach (var review in reviews)
            {
                DBResult result = Update(review);
                if (!result.Success)
                {
                    return new DBResult(false, $"{rowsAffected} rows affected", result.Exception);
                };
                rowsAffected++;
            }
            return new DBResult(true, $"{rowsAffected} rows affected");
        }

        public DBResult Delete(Review review)
        {
            return _connectionReviews.DeleteReview(review.Id);
        }

        public List<Review> Load(int count = 100, OrderByReviews? orderBy = null, int userID = 0, int feedID = 0, bool active = true)
        {
            return ConvertToDomainClass(_connectionReviews.LoadReviews(count, orderBy, userID, feedID, active));
        }

        #region Convert Methods
        public static ReviewDTO ConvertToDTO(Review review, int feedID = 0, int userID = 0)
        {
            ReviewDTO reviewDTO = new();
            reviewDTO.Id = review.Id;
            reviewDTO.Title = review.Title.Trim();
            reviewDTO.MainBody = review.MainBody.Trim();
            reviewDTO.UserID = userID;
            reviewDTO.FeedID = feedID;
            reviewDTO.Likes = review.Likes;

            return reviewDTO;
        }

        public static List<ReviewDTO> ConvertToDTO(List<Review> reviews, int feedID = 0, int userID = 0)
        {
            if (reviews == null || reviews.Count == 0) return null;

            List<ReviewDTO> listReviewDTOs = new();

            foreach (var review in reviews)
            {
                listReviewDTOs.Add(ConvertToDTO(review, feedID, userID));
            }

            return listReviewDTOs;
        }

        public static Review ConvertToDomainClass(ReviewDTO reviewDTO)
        {
            return new Review(reviewDTO.Id, reviewDTO.Title.Trim(), reviewDTO.MainBody.Trim(), reviewDTO.Likes);
        }

        public static List<Review> ConvertToDomainClass(List<ReviewDTO> reviewDTOs)
        {
            if (reviewDTOs == null || reviewDTOs.Count == 0) return new();

            List<Review> listReviews = new();

            foreach (var reviewDTO in reviewDTOs)
            {
                listReviews.Add(ConvertToDomainClass(reviewDTO));
            }

            return listReviews;
        }
        #endregion
    }
}
