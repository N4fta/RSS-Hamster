using Data.DatabaseConnections;
using Data.Interfaces;
using Domain.Algorithms.Modules;
using Domain.Interfaces;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Algorithms
{
    public class Algorithm
    {
        private List<IAlgoModule> algoModules;

        public Algorithm(List<IAlgoModule> modules)
        {
            algoModules = modules;
        }

        public List<Feed> GenerateRecommendation()
        {
            List<Feed> feeds = new List<Feed>();

            foreach (var module in algoModules)
            {
                feeds = module.Apply(feeds);
            }

            // If extra, cut them off
            if (feeds.Count > 20) feeds = feeds.GetRange(0, 20);
            return feeds;
        }
    }
}
