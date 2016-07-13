using MyMediaLite.Data;
using Recommender.Common.Logger;
using Recommender.Service.Data;
using System.Threading;

namespace Recommender.Service
{
    public interface IRatingService
    {
        Logger Logger { get; set; }

        void LoadFeaturedData(out IRatings _trainingData, out IRatings _testData, double ratio, int numberOfUsers, int minimumItemsRated, CancellationToken token);

        void LoadBasicData(out IRatings _trainingData, out IRatings _testData, double ratio, int numberOfUsers, int minimumItemsRated, CancellationToken token);

        void LoadComplexData(out IFeaturedRatings featuredTrainingData, out IFeaturedRatings featuredTestData, out IRatings simpleTrainingData, double ratio, int numberOfUsers, int numberForSimpleData, int minimumItemsRated, CancellationToken token);

        void LoadBasicData(out IRatings _trainingData, out IRatings _testData, double ratio);
    }
}
