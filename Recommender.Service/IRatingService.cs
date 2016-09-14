using MyMediaLite.Data;
using Recommender.Common.Logger;
using Recommender.Service.Data;
using System.Collections.Generic;
using System.Threading;

namespace Recommender.Service
{
    public interface IRatingService
    {
        Logger Logger { get; set; }

        IFeaturedRatings LoadFeaturedData(int numberOfUsers, int minimumItemsRated, CancellationToken token);

        IRatings LoadBasicData(int numberOfUsers, int minimumItemsRated, CancellationToken token);

        void LoadComplexData(out IFeaturedRatings featuredData, out IRatings simpleData, int numberOfUsers, int numberForSimpleData, int minimumItemsRated, CancellationToken token);


    }
}
