using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAlgoModule
    {
        public string Name { get; set; }

        public List<Feed> Apply(List<Feed> feeds);
    }
}
