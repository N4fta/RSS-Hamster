using Data.DTOs;
using Domain;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnitTests.Mock_Classes;

namespace UnitTests.Tests
{
    [TestClass]
    public class FeedServiceTests
    {
        [TestMethod]
        public void CheckIfFeedExistsFalse_Test()
        {
            // Repo
            var feedRepo = new Dictionary<int, FeedDTO>();
            var feedDTO = new FeedDTO();
            feedDTO.Id = 1;
            feedDTO.Name = "Test Feed1";
            feedDTO.Source = "example.com";
            feedDTO.ParserID = 0;
            feedDTO.ItemParserID = 0;
            feedRepo.Add(feedDTO.Id, feedDTO);
            
            var mockDbFeeds = new MockDbFeeds(feedRepo, new(), new(), new());

            // Others
            var feedService = new FeedService(mockDbFeeds);

            string source = "test";
            string name = "test";

            // Act
            var result = feedService.CheckIfFeedExists(source, name);

            // Assert
            Assert.IsFalse(result.Success);
        }
        [TestMethod]
        public void CheckIfFeedExistsTrue_Test()
        {
            // Repo
            var feedRepo = new Dictionary<int, FeedDTO>();
            var feedDTO = new FeedDTO();
            feedDTO.Id = 1;
            feedDTO.Name = "Test Feed1";
            feedDTO.Source = "example.com";
            feedDTO.ParserID = 0;
            feedDTO.ItemParserID = 0;
            feedRepo.Add(feedDTO.Id, feedDTO);

            var mockDbFeeds = new MockDbFeeds(feedRepo, new(), new(), new());

            // Others
            var feedService = new FeedService(mockDbFeeds);

            string source = "example.com";
            string name = "Test Feed1";

            // Act
            var result = feedService.CheckIfFeedExists(source, name);

            // Assert
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void AddFeed_Test()
        {
            // Repo
            var feedRepo = new Dictionary<int, FeedDTO>();
            var mockDbFeeds = new MockDbFeeds(feedRepo, new(), new(), new());

            // Others
            var feedService = new FeedService(mockDbFeeds);

            var feed = new Feed(
                1,
                "Test Feed1",
                "example.com",
                new(),
                new()
                );

            // Act
            var result = feedService.Add(feed);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(feedRepo.Count == 1);
            Assert.IsTrue(feedRepo.First().Value.Id == 1);
        }

        [TestMethod]
        public void UpdateFeed_Test()
        {
            // Repo
            var feedRepo = new Dictionary<int, FeedDTO>();
            var feedDTO = new FeedDTO();
            feedDTO.Id = 1;
            feedDTO.Name = "Test Feed1";
            feedDTO.Source = "example.com";
            feedDTO.ParserID = 0;
            feedDTO.ItemParserID = 0;
            feedRepo.Add(feedDTO.Id, feedDTO);

            var mockDbFeeds = new MockDbFeeds(feedRepo, new(), new(), new());

            // Others
            var feedService = new FeedService(mockDbFeeds);

            var feed = new Feed(
                1,
                "Test Feed Updated",
                "example2.com",
                new(),
                new()
                );

            // Act
            var result = feedService.Update(feed);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(feedRepo[feedDTO.Id] != feedDTO);
        }

        [TestMethod]
        public void DeleteFeed_Test()
        {
            // Repo
            var feedRepo = new Dictionary<int, FeedDTO>();
            var feedDTO = new FeedDTO();
            feedDTO.Id = 1;
            feedDTO.Name = "Test Feed1";
            feedDTO.Source = "example.com";
            feedDTO.ParserID = 0;
            feedDTO.ItemParserID = 0;
            feedRepo.Add(feedDTO.Id, feedDTO);

            var mockDbFeeds = new MockDbFeeds(feedRepo, new(), new(), new());

            // Others
            var feedService = new FeedService(mockDbFeeds);

            var feed = new Feed(
                1,
                "Test Feed1",
                "example.com",
                new(),
                new()
                );

            // Act
            var result = feedService.Delete(feed);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(feedRepo.Count == 0);
        }

        [TestMethod]
        public void LoadFeedAsync_Test()
        {
            // Repo
            var feedRepo = new Dictionary<int, FeedDTO>();
            var feedDTO1 = new FeedDTO();
            feedDTO1.Id = 1;
            feedDTO1.Name = "Test Feed1";
            feedDTO1.Source = "example.com";
            feedDTO1.ParserID = 0;
            feedDTO1.ItemParserID = 0;
            feedRepo.Add(feedDTO1.Id, feedDTO1);
            var feedDTO2 = new FeedDTO();
            feedDTO2.Id = 2;
            feedDTO2.Name = "Test Feed2";
            feedDTO2.Source = "example2.com";
            feedDTO2.ParserID = 0;
            feedDTO2.ItemParserID = 0;
            feedRepo.Add(feedDTO2.Id, feedDTO2);

            var mockDbFeeds = new MockDbFeeds(feedRepo, new(), new(), new());

            // Others
            var feedService = new FeedService(mockDbFeeds);

            // Act
            var result = feedService.LoadFeedAsync(2).Result;

            // Assert
            Assert.IsTrue(result.Id == 2);
        }

        [TestMethod]
        public void LoadFeedsAsync_Test()
        {
            // Repo
            var feedRepo = new Dictionary<int, FeedDTO>();
            var feedDTO1 = new FeedDTO();
            feedDTO1.Id = 1;
            feedDTO1.Name = "Test Feed1";
            feedDTO1.Source = "example.com";
            feedDTO1.ParserID = 0;
            feedDTO1.ItemParserID = 0;
            feedRepo.Add(feedDTO1.Id, feedDTO1);
            var feedDTO2 = new FeedDTO();
            feedDTO2.Id = 2;
            feedDTO2.Name = "Test Feed2";
            feedDTO2.Source = "example2.com";
            feedDTO2.ParserID = 0;
            feedDTO2.ItemParserID = 0;
            feedRepo.Add(feedDTO2.Id, feedDTO2);

            var mockDbFeeds = new MockDbFeeds(feedRepo, new(), new(), new());

            // Others
            var feedService = new FeedService(mockDbFeeds);

            // Act
            var result = feedService.LoadFeedsAsync().Result;

            // Assert
            Assert.IsTrue(result.Count == 2);
        }

        [TestMethod]
        public void LoadFeeds_Test()
        {
            // Repo
            var feedRepo = new Dictionary<int, FeedDTO>();
            var feedDTO1 = new FeedDTO();
            feedDTO1.Id = 1;
            feedDTO1.Name = "Test Feed1";
            feedDTO1.Source = "example.com";
            feedDTO1.ParserID = 0;
            feedDTO1.ItemParserID = 0;
            feedRepo.Add(feedDTO1.Id, feedDTO1);
            var feedDTO2 = new FeedDTO();
            feedDTO2.Id = 2;
            feedDTO2.Name = "Test Feed2";
            feedDTO2.Source = "example2.com";
            feedDTO2.ParserID = 0;
            feedDTO2.ItemParserID = 0;
            feedRepo.Add(feedDTO2.Id, feedDTO2);

            var mockDbFeeds = new MockDbFeeds(feedRepo, new(), new(), new());

            // Others
            var feedService = new FeedService(mockDbFeeds);

            // Act
            var result = feedService.LoadFeedsAlgo();

            // Assert
            Assert.IsTrue(result.Count == 2);
        }
    }
}
