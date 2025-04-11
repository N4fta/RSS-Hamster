using Domain.Algorithms;
using Domain.Algorithms.Modules;
using Domain.Services;

namespace Integration_Tests
{
    [TestClass]
    public class AlgorithmTests
    {
        [TestMethod]
        public void DefaultAlgoTest()
        {
            // Arrange
            var testUser = new UserService().GetUser("TestUser@email.com");
            var feedService = new FeedService();
            var userService = new UserService();

            var algoModules = AlgoModuleFactory.GetAlgoModule(
                new List<AlgoModule>()
                {
                    AlgoModule.BasedOnLikedFeedsCategories,
                    AlgoModule.BasedOnPublishers,
                    AlgoModule.BasedOnLikedReviews
                },
                testUser,
                feedService,
            userService
                );
            var defaultAlgo = new Algorithm(algoModules);

            // Act
            var resultFeeds = defaultAlgo.GenerateRecommendation();

            // Assert
            Assert.IsTrue(resultFeeds.Count == 20);
        }
    }
}