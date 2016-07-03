using MyMediaLite.RatingPrediction;
using Recommender.Core.MachineLearning;
using System.ComponentModel.DataAnnotations;

namespace Recommender.Core.Engine
{
    public class ContentRecommenderEngine : RecommenderEngine
    {
        //todo make Recommender obligatry to be of type IFeaturedPredictor

        private int _numberOfUsers = 1;

        [Range(1, int.MaxValue)]
        public int NumberOfUsers
        {
            get { return _numberOfUsers; }
            set { _numberOfUsers = value; }
        }

        [Range(1, int.MaxValue)]
        public int MinimumItemsRated { get; set; }


        public override void LoadData()
        {
            _service.LoadFeaturedData(out _trainingData, out _testData, _trainingSetRatio, _numberOfUsers, MinimumItemsRated);
        }
    }
}
