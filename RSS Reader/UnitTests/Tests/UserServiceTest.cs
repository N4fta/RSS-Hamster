using Data.DTOs;
using Domain;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Mock_Classes;

namespace UnitTests.Tests
{
    [TestClass]
    public class UserServiceTest
    {
        #region Simple Tests
        [TestMethod]
        public void CheckIfUserExistsTrue_Test()
        {
            // Repo
            var userRepo = new Dictionary<int, UserDTO>();
            var userDTO = new UserDTO();
            userDTO.Id = 1;
            userDTO.Name = "John Doe";
            userDTO.Email = "john@doe.com";
            userDTO.Username = "jonnhyDoe";
            userRepo.Add(1, userDTO);
            var mockDbUsers = new MockDbUsers(userRepo, new(), new(), new(), new(), new(), new());

            // Others
            var userService = new UserService(mockDbUsers);

            // Act
            var result = userService.CheckIfUserExists("john@doe.com", "jonnhyDoe");

            // Assert
            Assert.IsTrue(result.Success);
        }
        [TestMethod]
        public void CheckIfUserExistsFalse_Test()
        {
            // Repo
            var userRepo = new Dictionary<int, UserDTO>();
            var userDTO = new UserDTO();
            userDTO.Id = 1;
            userDTO.Name = "John Doe";
            userDTO.Email = "john@doe.com";
            userDTO.Username = "jonnhyDoe";
            userRepo.Add(1, userDTO);
            var mockDbUsers = new MockDbUsers(userRepo, new(), new(), new(), new(), new(), new());

            // Others
            var userService = new UserService(mockDbUsers);

            // Act
            var result = userService.CheckIfUserExists("john2@doe.com", "jonnhyDoe2");

            // Assert
            Assert.IsFalse(result.Success);
        }
        [TestMethod]
        public void AddUser_Test()
        {
            // Repo
            var userRepo = new Dictionary<int, UserDTO>();
            var mockDbUsers = new MockDbUsers(userRepo, new(), new(), new(), new(), new(), new());

            // Others
            var user = new User(
                "John Doe",
                "jonnhyDoe",
                "john@doe.com",
                "password",
                "",
                "Reader",
                new()
                );

            var userService = new UserService(mockDbUsers);

            // Act
            var result = userService.Add(user);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(userRepo.Count == 1);
        }
        [TestMethod]
        public void UpdateUser_Test()
        {
            // Repo
            var userRepo = new Dictionary<int, UserDTO>();
            var userDTO = new UserDTO();
            userDTO.Id = 1;
            userDTO.Name = "John Doe";
            userDTO.Email = "john@doe.com";
            userDTO.Username = "jonnhyDoe";
            userRepo.Add(1, userDTO);
            var mockDbUsers = new MockDbUsers(userRepo, new(), new(), new(), new(), new(), new());

            // Others
            var user = new User(
                1,
                "John Doe2",
                "jonnhyDoe2",
                "john@doe.com2",
                "password2",
                "",
                "Reader",
                new(),
                new()
                );

            var userService = new UserService(mockDbUsers);

            // Act
            var result = userService.Update(user);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(userRepo[1] != userDTO);
        }
        [TestMethod]
        public void DeleteUser_Test()
        {
            // Repo
            var userRepo = new Dictionary<int, UserDTO>();
            var userDTO = new UserDTO();
            userDTO.Id = 1;
            userDTO.Name = "John Doe";
            userDTO.Email = "john@doe.com";
            userDTO.Username = "jonnhyDoe";
            userRepo.Add(1, userDTO);
            var mockDbUsers = new MockDbUsers(userRepo, new(), new(), new(), new(), new(), new());

            // Others
            var user = new User(
                1,
                "John Doe",
                "jonnhyDoe",
                "john@doe.com",
                "password",
                "",
                "Reader",
                new(),
                new()
                );

            var userService = new UserService(mockDbUsers);

            // Act
            var result = userService.Delete(user);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(userRepo.Count == 0);
        }


        [TestMethod]
        public void CheckIfFeedLikedTrue_Test()
        {
            // Repo
            // User
            var userRepo = new Dictionary<int, UserDTO>();
            var userDTO = new UserDTO();
            userDTO.Id = 1;
            userDTO.Name = "John Doe";
            userDTO.Email = "john@doe.com";
            userDTO.Username = "jonnhyDoe";
            userRepo.Add(1, userDTO);
            // Feed
            var feedRepo = new Dictionary<int, FeedDTO>();
            var feedDTO = new FeedDTO();
            feedDTO.Id = 1;
            feedDTO.Name = "John Doe";
            feedRepo.Add(1, feedDTO);
            // User_LikedFeeds
            var userLikedFeeds = new List<KeyValuePair<int, int>>();
            userLikedFeeds.Add(new(1, 1));
            var mockDbUsers = new MockDbUsers(userRepo, feedRepo, new(), new(), userLikedFeeds, new(), new());

            // Others
            var userService = new UserService(mockDbUsers);

            // Act
            var result = userService.CheckIfFeedLiked(1, 1);

            // Assert
            Assert.IsTrue(result.Success);
        }
        [TestMethod]
        public void CheckIfFeedLikedFalse_Test()
        {

            // Repo
            // User
            var userRepo = new Dictionary<int, UserDTO>();
            var userDTO = new UserDTO();
            userDTO.Id = 1;
            userDTO.Name = "John Doe";
            userDTO.Email = "john@doe.com";
            userDTO.Username = "jonnhyDoe";
            userRepo.Add(1, userDTO);
            // Feed
            var feedRepo = new Dictionary<int, FeedDTO>();
            var feedDTO = new FeedDTO();
            feedDTO.Id = 1;
            feedDTO.Name = "John Doe";
            feedRepo.Add(1, feedDTO);
            // User_LikedFeeds
            var userLikedFeeds = new List<KeyValuePair<int, int>>();
            var mockDbUsers = new MockDbUsers(userRepo, feedRepo, new(), new(), userLikedFeeds, new(), new());

            // Others
            var userService = new UserService(mockDbUsers);

            // Act
            var result = userService.CheckIfFeedLiked(1, 1);

            // Assert
            Assert.IsFalse(result.Success);
        }
        [TestMethod]
        public void AddLikedFeed_Test()
        {
            // Repo
            // User
            var userRepo = new Dictionary<int, UserDTO>();
            var userDTO = new UserDTO();
            userDTO.Id = 1;
            userDTO.Name = "John Doe";
            userDTO.Email = "john@doe.com";
            userDTO.Username = "jonnhyDoe";
            userRepo.Add(1, userDTO);
            // Feed
            var feedRepo = new Dictionary<int, FeedDTO>();
            var feedDTO = new FeedDTO();
            feedDTO.Id = 1;
            feedDTO.Name = "John Doe";
            feedRepo.Add(1, feedDTO);
            // User_LikedFeeds
            var userLikedFeeds = new List<KeyValuePair<int, int>>();
            var mockDbUsers = new MockDbUsers(userRepo, feedRepo, new(), new(), userLikedFeeds, new(), new());

            // Others
            var userService = new UserService(mockDbUsers);

            // Act
            var result = userService.AddLikedFeed(1, 1);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(userLikedFeeds.Count == 1);
            Assert.IsTrue(userLikedFeeds.First().Key == 1 && userLikedFeeds.First().Value == 1);
        }
        // Not used
        //[TestMethod]
        //public void UpdateLikedFeed_Test()
        //{
        //    // Arrange


        //    // Act


        //    // Assert
        //    Assert.IsTrue(true);
        //}
        [TestMethod]
        public void RemoveLikedFeed_Test()
        {
            // Repo
            // User
            var userRepo = new Dictionary<int, UserDTO>();
            var userDTO = new UserDTO();
            userDTO.Id = 1;
            userDTO.Name = "John Doe";
            userDTO.Email = "john@doe.com";
            userDTO.Username = "jonnhyDoe";
            userRepo.Add(1, userDTO);
            // Feed
            var feedRepo = new Dictionary<int, FeedDTO>();
            var feedDTO = new FeedDTO();
            feedDTO.Id = 1;
            feedDTO.Name = "John Doe";
            feedRepo.Add(1, feedDTO);
            // User_LikedFeeds
            var userLikedFeeds = new List<KeyValuePair<int, int>>();
            userLikedFeeds.Add(new(1, 1));
            var mockDbUsers = new MockDbUsers(userRepo, feedRepo, new(), new(), userLikedFeeds, new(), new());

            // Others
            var userService = new UserService(mockDbUsers);

            // Act
            var result = userService.RemoveLikedFeed(1, 1);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(userLikedFeeds.Count == 0);
        }


        [TestMethod]
        public void AddLikedReview_Test()
        {
            // Repo
            // User
            var userRepo = new Dictionary<int, UserDTO>();
            var userDTO = new UserDTO();
            userDTO.Id = 1;
            userDTO.Name = "John Doe";
            userDTO.Email = "john@doe.com";
            userDTO.Username = "jonnhyDoe";
            userRepo.Add(1, userDTO);
            // Review
            var reviewRepo = new Dictionary<int, ReviewDTO>();
            var reviewDTO = new ReviewDTO();
            reviewDTO.Id = 1;
            reviewDTO.Title = "Test review";
            reviewRepo.Add(1, reviewDTO);
            // User_LikedReviews
            var userLikedReviews = new List<KeyValuePair<int, int>>();
            var mockDbUsers = new MockDbUsers(userRepo, new(), reviewRepo, new(), new(), userLikedReviews, new());

            // Others
            var userService = new UserService(mockDbUsers);

            // Act
            var result = userService.AddLikedReview(1, 1);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(userLikedReviews.Count == 1);
            Assert.IsTrue(userLikedReviews.First().Key == 1 && userLikedReviews.First().Value == 1);
        }
        // Not used
        //[TestMethod]
        //public void UpdateLikedReview_Test()
        //{
        //    // Arrange


        //    // Act


        //    // Assert
        //    Assert.IsTrue(true);
        //}
        [TestMethod]
        public void RemoveLikedReview_Test()
        {
            // Repo
            // User
            var userRepo = new Dictionary<int, UserDTO>();
            var userDTO = new UserDTO();
            userDTO.Id = 1;
            userDTO.Name = "John Doe";
            userDTO.Email = "john@doe.com";
            userDTO.Username = "jonnhyDoe";
            userRepo.Add(1, userDTO);
            // Review
            var reviewRepo = new Dictionary<int, ReviewDTO>();
            var reviewDTO = new ReviewDTO();
            reviewDTO.Id = 1;
            reviewDTO.Title = "Test review";
            reviewRepo.Add(1, reviewDTO);
            // User_LikedReviews
            var userLikedReviews = new List<KeyValuePair<int, int>>();
            userLikedReviews.Add(new(1, 1));
            var mockDbUsers = new MockDbUsers(userRepo, new(), reviewRepo, new(), new(), userLikedReviews, new());

            // Others
            var userService = new UserService(mockDbUsers);

            // Act
            var result = userService.RemoveLikedReview(1, 1);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(userLikedReviews.Count == 0);
        }
        #endregion

        #region Loading (and Convertion Methods) Tests
        [TestMethod]
        public void GetUser_Test()
        {
            // Repo
            var userRepo = new Dictionary<int, UserDTO>();
            var userDTO = new UserDTO();
            userDTO.Id = 1;
            userDTO.Name = "John Doe";
            userDTO.Email = "john@doe.com";
            userDTO.Username = "jonnhyDoe";
            userDTO.HashedPassword = "password";
            userDTO.Notes = "";
            userDTO.Role = "Reader";
            userRepo.Add(1, userDTO);
            var mockDbUsers = new MockDbUsers(userRepo, new(), new(), new(), new(), new(), new());

            // Others
            var userService = new UserService(mockDbUsers);

            // Act
            var result = userService.GetUser("john@doe.com");

            // Assert
            Assert.IsTrue(result is User);
            Assert.IsTrue(result.Id == userDTO.Id && result.Email == userDTO.Email);
        }
        [TestMethod]
        public void GetAll_Test()
        {
            // Repo
            var userRepo = new Dictionary<int, UserDTO>();
            var userDTO1 = new UserDTO();
            userDTO1.Id = 1;
            userDTO1.Name = "John Doe";
            userDTO1.Email = "john@doe.com";
            userDTO1.Username = "jonnhyDoe";
            userDTO1.HashedPassword = "password";
            userDTO1.Notes = "";
            userDTO1.Role = "Reader";
            userRepo.Add(1, userDTO1);
            var userDTO2 = new UserDTO();
            userDTO2.Id = 2;
            userDTO2.Name = "Jane Doe";
            userDTO2.Email = "jane@doe.com";
            userDTO2.HashedPassword = "password";
            userDTO2.Notes = "";
            userDTO2.Username = "jannyDoe";
            userDTO2.Role = "Reader";
            userRepo.Add(2, userDTO2);
            var mockDbUsers = new MockDbUsers(userRepo, new(), new(), new(), new(), new(), new());

            // Others
            var userService = new UserService(mockDbUsers);

            // Act
            var result = userService.GetAll();

            // Assert
            Assert.IsTrue(result.Count == 2);
        }


        // Has Problems due to User Service instanciating a Feed Service class
        // to convert FeedDTOs to Feeds
        public void LoadLikedFeeds_Test()
        {
            // Repo
            // User
            var userRepo = new Dictionary<int, UserDTO>();
            var userDTO = new UserDTO();
            userDTO.Id = 1;
            userDTO.Name = "John Doe";
            userDTO.Email = "john@doe.com";
            userDTO.Username = "jonnhyDoe";
            userDTO.HashedPassword = "password";
            userDTO.Notes = "";
            userRepo.Add(1, userDTO);
            // Feeds
            var feedRepo = new Dictionary<int, FeedDTO>();
            var feedDTO1 = new FeedDTO();
            feedDTO1.Id = 1;
            feedDTO1.Name = "John Doe";
            feedDTO1.Source = "example.com";
            feedDTO1.ParserID = 0;
            feedDTO1.ItemParserID = 0;
            feedRepo.Add(1, feedDTO1);
            var feedDTO2 = new FeedDTO();
            feedDTO2.Id = 2;
            feedDTO2.Name = "John Doe2";
            feedDTO2.Source = "example2.com";
            feedDTO2.ParserID = 0;
            feedDTO2.ItemParserID = 0;
            feedRepo.Add(2, feedDTO2);
            // User_LikedFeeds
            var userLikedFeeds = new List<KeyValuePair<int, int>>();
            userLikedFeeds.Add(new(1, 2));
            var mockDbUsers = new MockDbUsers(userRepo, feedRepo, new(), new(), userLikedFeeds, new(), new());

            // Others
            var userService = new UserService(mockDbUsers);

            // Act
            var result = userService.LoadLikedFeeds(1).Result;

            // Assert
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.First().Id == 2);
        }


        [TestMethod]
        public void LoadLikedReviews_Test()
        {
            // Repo
            // User
            var userRepo = new Dictionary<int, UserDTO>();
            var userDTO = new UserDTO();
            userDTO.Id = 1;
            userDTO.Name = "John Doe";
            userDTO.Email = "john@doe.com";
            userDTO.Username = "jonnhyDoe";
            userDTO.HashedPassword = "password";
            userDTO.Notes = "";
            userRepo.Add(1, userDTO);
            // Reviews
            var reviewRepo = new Dictionary<int, ReviewDTO>();
            var reviewDTO1 = new ReviewDTO();
            reviewDTO1.Id = 1;
            reviewDTO1.Title = "Test review1";
            reviewDTO1.MainBody = "";
            reviewDTO1.FeedID = 1;
            reviewDTO1.UserID = 1;
            reviewDTO1.Likes = 0;
            reviewRepo.Add(1, reviewDTO1);
            var reviewDTO2 = new ReviewDTO();
            reviewDTO2.Id = 2;
            reviewDTO2.Title = "Test review2";
            reviewDTO2.MainBody = "";
            reviewDTO2.FeedID = 1;
            reviewDTO2.UserID = 1;
            reviewDTO2.Likes = 0;
            reviewRepo.Add(2, reviewDTO2);
            // User_LikedReviews
            var userLikedReviews = new List<KeyValuePair<int, int>>();
            userLikedReviews.Add(new(1, 2));
            var mockDbUsers = new MockDbUsers(userRepo, new(), reviewRepo, new(), new(), userLikedReviews, new());

            // Others
            var userService = new UserService(mockDbUsers);

            // Act
            var result = userService.LoadLikedReviews(1);

            // Assert
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.First().Id == 2);
        }


        [TestMethod]
        public void LoadLikedUsers_Test()
        {
            // Repo
            // User
            var userRepo = new Dictionary<int, UserDTO>();
            var userDTO1 = new UserDTO();
            userDTO1.Id = 1;
            userDTO1.Name = "John Doe";
            userDTO1.Email = "john@doe.com";
            userDTO1.Username = "jonnhyDoe";
            userDTO1.Role = "Reader";
            userRepo.Add(1, userDTO1);
            var userDTO2 = new UserDTO();
            userDTO2.Id = 2;
            userDTO2.Name = "Jane Doe";
            userDTO2.Email = "jane@doe.com";
            userDTO2.Username = "jannyDoe";
            userDTO2.Role = "Reader";
            userRepo.Add(2, userDTO2);
            // Feeds
            var feedRepo = new Dictionary<int, FeedDTO>();
            var feedDTO1 = new FeedDTO();
            feedDTO1.Id = 1;
            feedDTO1.Name = "John Doe";
            feedRepo.Add(1, feedDTO1);
            var feedDTO2 = new FeedDTO();
            feedDTO2.Id = 2;
            feedDTO2.Name = "John Doe2";
            feedRepo.Add(2, feedDTO2);
            // User_LikedFeeds
            var userLikedFeeds = new List<KeyValuePair<int, int>>();
            userLikedFeeds.Add(new(1, 2));
            // Reviews
            var reviewRepo = new Dictionary<int, ReviewDTO>();
            var reviewDTO1 = new ReviewDTO();
            reviewDTO1.Id = 1;
            reviewDTO1.Title = "Test review1";
            reviewDTO1.UserID = 1;
            reviewDTO1.FeedID = 1;
            reviewRepo.Add(1, reviewDTO1);
            var reviewDTO2 = new ReviewDTO();
            reviewDTO2.Id = 2;
            reviewDTO2.Title = "Test review2";
            reviewDTO2.UserID = 2;
            reviewDTO2.FeedID = 2;
            reviewRepo.Add(2, reviewDTO2);
            // User_LikedReviews
            var userLikedReviews = new List<KeyValuePair<int, int>>();
            userLikedReviews.Add(new(1, 2));

            var mockDbUsers = new MockDbUsers(userRepo, feedRepo, reviewRepo, new(), new(), userLikedReviews, new());

            // Others
            var userService = new UserService(mockDbUsers);

            // Act
            var result = userService.LoadLikedUsers(1);

            // Assert
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.First() == 2);
        }
        #endregion
    }
}
