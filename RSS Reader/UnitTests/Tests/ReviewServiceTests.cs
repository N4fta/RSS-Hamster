using Data.DTOs;
using Domain;
using Domain.Services;
using UnitTests.Mock_Classes;

namespace UnitTests.Tests
{
    [TestClass]
    public class ReviewServiceTests
    {
        [TestMethod]
        public void AddReview_Test()
        {
            // Repo
            var reviewRepo = new Dictionary<int, ReviewDTO>();
            var mockDbReviews = new MockDbReviews(reviewRepo);

            // Others
            var review = new Review(
                1,
                "Test Review",
                "",
                0
                );

            var reviewService = new ReviewService(mockDbReviews);

            // Act
            var result = reviewService.Add(review, 1, 1);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(reviewRepo.Count == 1);
        }

        [TestMethod]
        public void UpdateReview_Test()
        {
            // Repo
            var reviewRepo = new Dictionary<int, ReviewDTO>();
            var reviewDTO = new ReviewDTO();
            reviewDTO.Id = 1;
            reviewDTO.Title = "Test Review";
            reviewDTO.MainBody = "";
            reviewDTO.UserID = 1;
            reviewDTO.FeedID = 1;
            reviewDTO.Likes = 0;
            reviewRepo.Add(reviewDTO.Id, reviewDTO);
            var mockDbReviews = new MockDbReviews(reviewRepo);

            // Others
            var review = new Review(
                1,
                "Test Review2",
                "",
                10
                );

            var reviewService = new ReviewService(mockDbReviews);

            // Act
            var result = reviewService.Update(review);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(reviewRepo.First().Value != reviewDTO);
        }

        [TestMethod]
        public void UpdateMultipleReviews_Test()
        {
            // Repo
            var reviewRepo = new Dictionary<int, ReviewDTO>();
            var reviewDTO1 = new ReviewDTO();
            reviewDTO1.Id = 1;
            reviewDTO1.Title = "Test Review";
            reviewDTO1.MainBody = "";
            reviewDTO1.UserID = 1;
            reviewDTO1.FeedID = 1;
            reviewDTO1.Likes = 0;
            reviewRepo.Add(reviewDTO1.Id, reviewDTO1);
            var reviewDTO2 = new ReviewDTO();
            reviewDTO2.Id = 2;
            reviewDTO2.Title = "Test Review2";
            reviewDTO2.MainBody = "";
            reviewDTO2.UserID = 2;
            reviewDTO2.FeedID = 2;
            reviewDTO2.Likes = 2;
            reviewRepo.Add(reviewDTO2.Id, reviewDTO2);
            var mockDbReviews = new MockDbReviews(reviewRepo);

            // Others
            var review1 = new Review(
                1,
                "Updated Test Review1",
                "",
                1
                );
            var review2 = new Review(
                2,
                "Updated Test Review2",
                "",
                4
                );
            var toUpdate = new List<Review> { review1, review2 };

            var reviewService = new ReviewService(mockDbReviews);

            // Act
            var result = reviewService.Update(toUpdate);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(reviewRepo[reviewDTO1.Id] != reviewDTO1);
            Assert.IsTrue(reviewRepo[reviewDTO2.Id] != reviewDTO2);
        }

        [TestMethod]
        public void DeleteReview_Test()
        {
            // Repo
            var reviewRepo = new Dictionary<int, ReviewDTO>();
            var reviewDTO = new ReviewDTO();
            reviewDTO.Id = 1;
            reviewDTO.Title = "Test Review";
            reviewDTO.MainBody = "";
            reviewDTO.UserID = 1;
            reviewDTO.FeedID = 1;
            reviewDTO.Likes = 0;
            reviewRepo.Add(reviewDTO.Id, reviewDTO);
            var mockDbReviews = new MockDbReviews(reviewRepo);

            // Others
            var review = new Review(
                1,
                "Test Review",
                "",
                10
                );

            var reviewService = new ReviewService(mockDbReviews);

            // Act
            var result = reviewService.Delete(review);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(reviewRepo.Count == 0);
        }

        [TestMethod]
        public void LoadReviews_Test()
        {
            // Repo
            var reviewRepo = new Dictionary<int, ReviewDTO>();
            var reviewDTO1 = new ReviewDTO();
            reviewDTO1.Id = 1;
            reviewDTO1.Title = "Test Review";
            reviewDTO1.MainBody = "";
            reviewDTO1.UserID = 1;
            reviewDTO1.FeedID = 1;
            reviewDTO1.Likes = 0;
            reviewRepo.Add(reviewDTO1.Id, reviewDTO1);
            var reviewDTO2 = new ReviewDTO();
            reviewDTO2.Id = 2;
            reviewDTO2.Title = "Test Review2";
            reviewDTO2.MainBody = "";
            reviewDTO2.UserID = 2;
            reviewDTO2.FeedID = 2;
            reviewDTO2.Likes = 2;
            reviewRepo.Add(reviewDTO2.Id, reviewDTO2);
            var mockDbReviews = new MockDbReviews(reviewRepo);

            // Others
            var reviewService = new ReviewService(mockDbReviews);

            // Act
            var result = reviewService.Load();

            // Assert
            Assert.IsTrue(result.Count == 2);
        }
    }
}
