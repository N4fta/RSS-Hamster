using Domain;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Mock_Classes.Algorithm
{
    internal class MockAlgoModule : IAlgoModule
    {
        public string Name { get; set; } = "Mock Module";
        private List<Feed> _feeds { get; set; }

        public MockAlgoModule(List<Feed> feeds)
        {
            _feeds = feeds;
        }

        public List<Feed> Apply(List<Feed> feeds)
        {
            return _feeds;
        }
    }
}
