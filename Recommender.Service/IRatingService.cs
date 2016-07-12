using MyMediaLite.Data;
using Recommender.Service.Data;
using System.Threading;

namespace Recommender.Service
{
    public interface IRatingService
    {
        void LoadFeaturedData(out IRatings _trainingData, out IRatings _testData, double ratio, int numberOfUsers, int minimumItemsRated, CancellationToken token);

        void LoadBasicData(out IRatings _trainingData, out IRatings _testData, double ratio, int numberOfUsers, int minimumItemsRated, CancellationToken token);
        void LoadBasicData(out IRatings _trainingData, out IRatings _testData, double ratio);
    }
}
