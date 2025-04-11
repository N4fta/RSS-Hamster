using Data.Interfaces;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DatabaseConnections;
using Data.DTOs;
using Domain.Interfaces;
using Domain.FeedParsers;
using Domain.FeedParsers.FeedItemParsers;
using System.Net.Http.Headers;
using CodeHollow.FeedReader;

namespace Domain.Services
{
    public class FeedService
    {
        private IDbConnectionFeeds _connectionFeeds = new DbConnectionFeeds();
        private static ParserService _parsersService = new();

        public FeedService()
        {
        }
        public FeedService(IDbConnectionFeeds dbConnectionFeeds)
        {
            _connectionFeeds = dbConnectionFeeds;
        }

        public DBResult Add(Feed feed)
        {
            return _connectionFeeds.InsertFeed(ConvertToDTO(feed));
        }

        public DBResult CheckIfFeedExists(string source, string name)
        {
            // Returns false if both Name and Source are not taken
            // In the other cases message contains reason
            return _connectionFeeds.CheckIfFeedExists(source, name);
        }

        public DBResult Update(Feed feed)
        {
            DBResult dbResult;

            // Update Categories
            dbResult = _connectionFeeds.UpdateFeedCategory(feed.Categories, feed.Id);
            if (!dbResult.Success) return dbResult;

            // Update Reviews
            dbResult = new ReviewService().Update(feed.Reviews);
            if (!dbResult.Success) return dbResult;

            return _connectionFeeds.UpdateFeed(ConvertToDTO(feed));
        }

        public DBResult Delete(Feed feed)
        {
            return _connectionFeeds.DeleteFeed(feed.Id);
        }

        public async Task<Feed?> LoadFeedAsync(int feedID, bool active = true)
        {
            Feed? feed = null;

            var feedDTO = await _connectionFeeds.LoadFeedAsync(feedID, active);
            feed = ConvertToDomainClass(feedDTO);
            return feed;
        }

        public async Task<List<Feed>> LoadFeedsAsync(string filter = "", int count = 2000, OrderByFeeds? orderBy = OrderByFeeds.Name, bool active = true)
        {
            List<FeedDTO> feedDTOs = new();

            feedDTOs = await _connectionFeeds.LoadFeedsAsync(filter, count, orderBy, active);

            var feeds = ConvertToDomainClass(feedDTOs);
            return feeds;
        }

        public List<Feed> LoadFeedsAlgo(int count = 100, List<string>? categoriesFilters = null, OrderByFeeds? orderBy = OrderByFeeds.Popularity, bool active = true)
        {
            return ConvertToDomainClass(_connectionFeeds.LoadFeedsAlgo(count, categoriesFilters, orderBy, active));
        }

        #region Convert Methods
        public static FeedDTO? ConvertToDTO(Feed feed)
        {
            // Get IDs for Parser Enums
            int feedParserID = _parsersService.GetParserID(feed.FeedParser);
            int itemParserID = _parsersService.GetItemParserID(feed.FeedItemParser);

            FeedDTO feedDTO = new();
            feedDTO.Id = feed.Id;
            feedDTO.Source = feed.Source.Trim();
            feedDTO.Name = feed.Name.Trim();
            feedDTO.ParserID = feedParserID;
            feedDTO.ItemParserID = itemParserID;
            feedDTO.Categories = feed.Categories;
            feedDTO.Reviews = ReviewService.ConvertToDTO(feed.Reviews);

            return feedDTO;
        }

        public static List<FeedDTO> ConvertToDTO(List<Feed> feeds)
        {
            if (feeds == null || feeds.Count == 0) return new();

            List<FeedDTO> listFeedDTOs = new();

            foreach (var feed in feeds)
            {
                listFeedDTOs.Add(ConvertToDTO(feed));
            }

            return listFeedDTOs;
        }

        public Feed? ConvertToDomainClass(FeedDTO feedDTO)
        {
            if (feedDTO == null) return null;

            var feedParser = _parsersService.GetParser(feedDTO.ParserID);
            var feedItemParser = _parsersService.GetItemParser(feedDTO.ItemParserID);

            List<ReviewDTO> reviews = new();
            // Purely for testing purposes (band aid solution)
            if (feedDTO.Id > 5) reviews = _connectionFeeds.LoadFeedReviews(feedDTO.Id);

            return new Feed(
                feedDTO.Id,
                feedDTO.Name.Trim(),
                feedDTO.Source.Trim(),
                ReviewService.ConvertToDomainClass(reviews),
                feedDTO.Categories.Select(cat => cat.Trim()).ToList(),
                feedParser,
                feedItemParser);
        }

        public List<Feed> ConvertToDomainClass(List<FeedDTO> feedDTOs)
        {
            if (feedDTOs == null || feedDTOs.Count == 0) return new();

            List<Feed> listFeeds = new();

            foreach (var feedDTO in feedDTOs)
            {
                listFeeds.Add(ConvertToDomainClass(feedDTO));
            }

            return listFeeds;
        }
        #endregion

    }
}
