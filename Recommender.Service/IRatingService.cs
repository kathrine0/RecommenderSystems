using MyMediaLite.Data;
using Recommender.Service.Data;

namespace Recommender.Service
{
    public interface IRatingService
    {
        void LoadFeaturedData(out IRatings _trainingData, out IRatings _testData, double ratio);
        void LoadBasicData(out IRatings _trainingData, out IRatings _testData, double ratio);
    }
}
