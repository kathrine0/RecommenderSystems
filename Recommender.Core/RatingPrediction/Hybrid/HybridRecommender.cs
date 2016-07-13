using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recommender.Common.Logger;
using MyMediaLite.RatingPrediction;
using MyMediaLite.Data;
using Recommender.Core.RatingPrediction.ContentBased;
using Recommender.Service.Data;

namespace Recommender.Core.RatingPrediction.Hybrid
{
    public class HybridRecommender : ILoggable, IHybridPredictor
    {
        public Logger Logger { get;set; }
        public bool RecommenderStatus { get; set; }
        public float MaxRating { get; set; }
        public float MinRating { get; set; }
        public IRatings Ratings { get; set; }

        private IRatingPredictor _collaborativeRecommender;

        private IFeaturedPredictor _contentRecommender;
        public IRatingPredictor CollaborativeRecommender
        {
            get { return _collaborativeRecommender; }
            set
            {
                _collaborativeRecommender = value;
                if (_collaborativeRecommender is ILoggable) ((ILoggable)_collaborativeRecommender).Logger = Logger;
            }
        }

        public IFeaturedPredictor ContentRecommender
        {
            get { return _contentRecommender; }
            set
            {
                _contentRecommender = value;
                if (_contentRecommender is ILoggable) ((ILoggable)_contentRecommender).Logger = Logger;
            }
        }

        public IRatings CollaborativeRatings { get; set; }
        public IFeaturedRatings ContentRatings { get; set; }

        public IFeaturedRatings FeaturedRatings
        {
            get { return ContentRatings; }
            set { throw new NotImplementedException();}
        }

        public HybridRecommender()
        {
            RecommenderStatus = true;
        }

        public bool CanPredict(int user_id, int item_id)
        {
            throw new NotImplementedException();
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public void LoadModel(string filename)
        {
            throw new NotImplementedException();
        }

        public float Predict(int user_id, int item_id)
        {
            throw new NotImplementedException();
        }
        public float Predict(int user_id, int item_id, IList<IFeature> features)
        {
            var contentPrediction = ContentRecommender.Predict(user_id, item_id, features);
            var collaborativePrediction = CollaborativeRecommender.Predict(user_id, item_id);

            var contentError = ContentRecommender.GetError(user_id);

            throw new NotImplementedException();
        }

        public IList<Tuple<int, float>> Recommend(int user_id, int n = -1, ICollection<int> ignore_items = null, ICollection<int> candidate_items = null)
        {
            throw new NotImplementedException();
        }

        public void SaveModel(string filename)
        {
            throw new NotImplementedException();
        }

        public void Train()
        {
            if (CollaborativeRecommender == null || ContentRecommender == null)
                throw new ArgumentNullException("No recommenders");

            if (CollaborativeRatings == null || ContentRatings == null)
                throw new ArgumentNullException("No data");


            Logger.AddProgressReport(new ProgressState(0, "Training collaborative recommender", "Training..."));

            CollaborativeRecommender.Ratings = CollaborativeRatings;
            CollaborativeRecommender.Train();

            Logger.AddProgressReport(new ProgressState(0, "Training content-based recommender", "Training..."));
            ContentRecommender.Ratings = ContentRatings;
            ContentRecommender.Train();
        }

        public double GetError(int user_id)
        {
            throw new NotImplementedException();
        }
    }
}
