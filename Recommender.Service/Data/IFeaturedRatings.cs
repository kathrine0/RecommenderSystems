using MyMediaLite.Data;
using System.Collections.Generic;

namespace Recommender.Service.Data
{
    public interface IFeaturedRatings : IRatings
    {
        IList<IDictionary<string, string>> Features { get; }

    }
}