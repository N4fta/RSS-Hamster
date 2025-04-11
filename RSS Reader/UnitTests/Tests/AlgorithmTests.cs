using Domain.Algorithms.Modules;
using Domain.Algorithms;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Mock_Classes.Algorithm;
using Domain.Interfaces;
using UnitTests.Mock_Classes;
using Domain;
using Data.DTOs;

namespace UnitTests.Tests
{
    [TestClass]
    public class AlgorithmTests
    {
        [TestMethod]
        public void ModuleReturnsEmptyList_Test()
        {
            // Arrange
            IAlgoModule algoModules = new MockAlgoModule(new());
            var defaultAlgo = new Algorithm(new() { algoModules });

            // Act
            var resultFeeds = defaultAlgo.GenerateRecommendation();

            // Assert
            Assert.IsTrue(resultFeeds.Count == 0);
        }

        [TestMethod]
        public void BasedOnPublishers_Test()
        {
            // Data
            // Users
            var userRepo = new Dictionary<int, UserDTO>();
            var userDTOs = GetExampleUserDTOs();
            foreach (var userDTO in userDTOs)
            {
                userRepo.Add(userDTO.Id, userDTO);
            }
            // Feeds
            var feedRepo = new Dictionary<int, FeedDTO>();
            var feedDTOs = GetExampleFeedDTOs();
            foreach (var feedDTO in feedDTOs)
            {
                feedRepo.Add(feedDTO.Id, feedDTO);
            }
            // User_LikedFeeds
            var userLikedFeeds = new List<KeyValuePair<int, int>>
            {
                new(1, 2)
            };

            var mockDbFeeds = new MockDbFeeds(feedRepo, new(), new(), new());
            var mockDbUsers = new MockDbUsers(userRepo, feedRepo, new(), new(), userLikedFeeds, new(), new());

            // Algorithm
            var user = GetExampleUser();

            IAlgoModule? algoModules = AlgoModuleFactory.GetAlgoModule(
                AlgoModule.BasedOnPublishers,
                user,
                new FeedService(mockDbFeeds),
                new UserService(mockDbUsers)
                );

            var defaultAlgo = new Algorithm(new() { algoModules });

            // Act
            var resultFeeds = defaultAlgo.GenerateRecommendation();

            // Assert
            // Should recommend (Feed 1 & Feed 2) and Add feed 3
            // at the end since there are less than 20 feeds to recommend
            Assert.IsTrue(resultFeeds.Count == 3);
            Assert.IsTrue(resultFeeds[0].Id == feedDTOs[0].Id || resultFeeds[1].Id == feedDTOs[0].Id);
        }

        [TestMethod]
        public void BasedOnLikedFeedsCategories_Test()
        {
            // Data
            // Users
            var userRepo = new Dictionary<int, UserDTO>();
            var userDTOs = GetExampleUserDTOs();
            foreach (var userDTO in userDTOs)
            {
                userRepo.Add(userDTO.Id, userDTO);
            }
            // Feeds
            var feedRepo = new Dictionary<int, FeedDTO>();
            var feedDTOs = GetExampleFeedDTOs();
            foreach (var feedDTO in feedDTOs)
            {
                feedRepo.Add(feedDTO.Id, feedDTO);
            }
            // User_LikedFeeds
            var userLikedFeeds = new List<KeyValuePair<int, int>>
            {
                new(1, 2)
            };
            // Categories
            var categories = GetExampleCategories();
            // Feeds_Categories
            var feeds_categories = new List<KeyValuePair<int, int>>
            {
                new(2, 4),
                new(2, 5),
                new(2, 6),
                new(1, 1),
                new(1, 2),
                new(1, 3),
                new(3, 4),
                new(3, 3),
                new(3, 7)
            };

            var mockDbUsers = new MockDbUsers(userRepo, feedRepo, new(), categories, userLikedFeeds, new(), new());
            var mockDbFeeds = new MockDbFeeds(feedRepo, categories, new(), feeds_categories);

            // Others
            var user = GetExampleUser();

            IAlgoModule? algoModules = AlgoModuleFactory.GetAlgoModule(
                AlgoModule.BasedOnLikedFeedsCategories,
                user,
                new FeedService(mockDbFeeds),
                new UserService(mockDbUsers)
                );

            var defaultAlgo = new Algorithm(new() { algoModules });

            // Act
            var resultFeeds = defaultAlgo.GenerateRecommendation();

            // Assert
            // Should recommend (Feed 2 & Feed 2) and Add feed 1
            // at the end since there are less than 20 feeds to recommend
            Assert.IsTrue(resultFeeds.Count == 3);
            Assert.IsTrue(resultFeeds[0].Id == feedDTOs[2].Id || resultFeeds[1].Id == feedDTOs[2].Id);
        }

        [TestMethod]
        public void BasedOnLikedReviews()
        {
            // Repo
            // Users
            var userRepo = new Dictionary<int, UserDTO>();
            var userDTOs = GetExampleUserDTOs();
            foreach (var userDTO in userDTOs)
            {
                userRepo.Add(userDTO.Id, userDTO);
            }
            // Feeds
            var feedRepo = new Dictionary<int, FeedDTO>();
            var feedDTOs = GetExampleFeedDTOs();
            foreach (var feedDTO in feedDTOs)
            {
                feedRepo.Add(feedDTO.Id, feedDTO);
            }
            // User_LikedFeeds
            var userLikedFeeds = new List<KeyValuePair<int, int>>
            {
                new(2, 3)
            };
            // Reviews
            var reviewRepo = new Dictionary<int, ReviewDTO>();
            var reviewDTO = GetExampleReviewDTO();
            reviewRepo.Add(reviewDTO.Id, reviewDTO);
            // User_LikedReviews
            var userLikedReviews = new List<KeyValuePair<int, int>>
            {
                new(1, 1)
            };

            var mockDbUsers = new MockDbUsers(userRepo, feedRepo, reviewRepo, new(), userLikedFeeds, userLikedReviews, new());
            var mockDbFeeds = new MockDbFeeds(feedRepo, new(), new(), new());

            // Others
            var user = GetExampleUser();

            IAlgoModule? algoModules = AlgoModuleFactory.GetAlgoModule(
                AlgoModule.BasedOnLikedReviews,
                user,
                new FeedService(mockDbFeeds),
                new UserService(mockDbUsers)
                );

            var defaultAlgo = new Algorithm(new() { algoModules });

            // Act
            var resultFeeds = defaultAlgo.GenerateRecommendation();

            // Assert
            // Should recommend Feed 3 and add feed 1 & 2
            // at the end since there are less than 20 feeds to recommend
            Assert.IsTrue(resultFeeds.Count == 3);
            Assert.IsTrue(resultFeeds[0].Id == feedDTOs[2].Id);
        }

        private User GetExampleUser()
        {
            return new User(
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
        }
        private List<UserDTO> GetExampleUserDTOs()
        {
            var userDTO1 = new UserDTO();
            userDTO1.Id = 1;
            userDTO1.Name = "John Doe";
            userDTO1.Email = "john@doe.com";
            userDTO1.Username = "jonnhyDoe";
            userDTO1.HashedPassword = "password";
            userDTO1.Notes = "";
            userDTO1.Role = "Reader";

            var userDTO2 = new UserDTO();
            userDTO2.Id = 2;
            userDTO2.Name = "Jane Doe";
            userDTO2.Email = "jane@doe.com";
            userDTO2.Username = "jannyDoe";
            userDTO2.HashedPassword = "password";
            userDTO2.Notes = "";
            userDTO2.Role = "Reader";

            return new List<UserDTO>() { userDTO1, userDTO2 };
        }
        private List<FeedDTO> GetExampleFeedDTOs()
        {
            var feedDTO1 = new FeedDTO();
            feedDTO1.Id = 1;
            feedDTO1.Name = "Feed1";
            feedDTO1.Source = "publisher1.com";
            feedDTO1.ParserID = 0;
            feedDTO1.ItemParserID = 0;
            feedDTO1.Categories = new List<string>
            {
                "AI",
                "Tech",
                "Podcast"
            };
            var feedDTO2 = new FeedDTO();
            feedDTO2.Id = 2;
            feedDTO2.Name = "Feed2";
            feedDTO2.Source = "publisher1.com";
            feedDTO2.ParserID = 0;
            feedDTO2.ItemParserID = 0;
            feedDTO2.Categories = new List<string>
            {
                "Sports",
                "News",
                "Culture"
            };
            var feedDTO3 = new FeedDTO();
            feedDTO3.Id = 3;
            feedDTO3.Name = "Feed3";
            feedDTO3.Source = "other-publisher.com";
            feedDTO3.ParserID = 0;
            feedDTO3.ItemParserID = 0;
            feedDTO3.Categories = new List<string>
            {
                "Sports",
                "Podcast",
                "Entertainment"
            };
            return new List<FeedDTO>() { feedDTO1, feedDTO2, feedDTO3 };
        }
        private ReviewDTO GetExampleReviewDTO()
        {
            var reviewDTO = new ReviewDTO();
            reviewDTO.Id = 1;
            reviewDTO.Title = "Test review1";
            reviewDTO.MainBody = "";
            reviewDTO.FeedID = 3;
            reviewDTO.UserID = 2;
            reviewDTO.Likes = 1;
            return reviewDTO;
        }
        private Dictionary<int, string> GetExampleCategories()
        {
            return new Dictionary<int, string>
            {
                { 1, "AI" },
                { 2, "Tech" },
                { 3, "Podcast" },
                { 4, "Sports" },
                { 5, "News" },
                { 6, "Culture" },
                { 7, "Entertainment" }
            };
        }
    }
}
