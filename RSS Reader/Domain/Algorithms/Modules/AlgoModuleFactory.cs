using Domain.Interfaces;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Algorithms.Modules
{
    public static class AlgoModuleFactory
    {
        // move services to constructor?

        public static IAlgoModule? GetAlgoModule(AlgoModule moduleName, User user, FeedService feedService, UserService userService)
        {
            IAlgoModule? module = null;

            if (moduleName == AlgoModule.BasedOnLikedReviews)
            {
                module = new BasedOnLikedReviews(user, feedService, userService);
            }
            else if (moduleName == AlgoModule.BasedOnLikedFeedsCategories)
            {
                module = new BasedOnLikedFeedsCategories(user, feedService, userService);
            }
            else if (moduleName == AlgoModule.BasedOnPublishers)
            {
                module = new BasedOnPublishers(user, feedService, userService);
            }

            return module;
        }

        public static List<IAlgoModule>? GetAlgoModule(List<AlgoModule> moduleNames, User user, FeedService feedService, UserService userService)
        {
            List<IAlgoModule>? modules = new();

            if (moduleNames == null || moduleNames.Count < 1) return modules;

            foreach (var moduleName in moduleNames)
            {
                modules.Add(GetAlgoModule(moduleName, user, feedService, userService));
            }

            return modules;
        }
    }

    public enum AlgoModule
    {
        BasedOnLikedFeedsCategories,
        BasedOnLikedReviews,
        BasedOnPublishers
    }
}
