using MyMediaLite.Data;
using MyMediaLite.Eval;
using MyMediaLite.RatingPrediction;
using Recommender.Common.Logger;
using Recommender.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender.Core.Engine
{
    /// <summary>
    /// This is the main application class
    /// </summary>
    public abstract class RecommenderEngine
    {
        public RecommenderEngine(Logger logger)
        {
            _logger = logger;
            _service = new MovieLenseService();
        }

        public virtual IRatingPredictor Recommender
        {
            get; set;
        }

        protected Logger _logger;


        protected IRatingService _service;
        protected IRatings _trainingData;
        protected IRatings _testData;

        protected double _trainingSetRatio = 0.8;

        public void SetTrainingSetRatio(decimal value)
        {
            if (value < 1 || value > 90)
                throw new ArgumentOutOfRangeException();

            _trainingSetRatio = (double) value / 100;
        }

        public abstract void LoadData();

        public void TeachRecommender()
        {
            if (Recommender == null)
                throw new ArgumentNullException("Recommender not set");

            // set up the recommender
            Recommender.Ratings = _trainingData;
            Recommender.Train();
        }

        public RatingPredictionEvaluationResults GetResults()
        {
            
            // measure the accuracy on the test data set
            return Recommender.Evaluate(_testData);
            
            // make a prediction for a certain user and item
            //Console.WriteLine(_recommender.Predict(1, 1));

            //var bmf = new BiasedMatrixFactorization { Ratings = training_data };
            //Console.WriteLine(bmf.DoCrossValidation());
        }
    }
}
