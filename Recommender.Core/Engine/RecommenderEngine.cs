using MyMediaLite.Data;
using MyMediaLite.Eval;
using MyMediaLite.RatingPrediction;
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
        public virtual IRatingPredictor Recommender
        {
            get; set;
        }

        protected MovieLenseService _service;
        protected IRatings _trainingData;
        protected IRatings _testData;


        public RecommenderEngine()
        {
            _service = new MovieLenseService();
        }

        public abstract void LoadData();

        public RatingPredictionEvaluationResults GetResults()
        {
            if (Recommender == null)
                throw new ArgumentNullException("Recommender not set");

            // set up the recommender
            Recommender.Ratings = _trainingData;
            Recommender.Train();

            // measure the accuracy on the test data set
            return Recommender.Evaluate(_testData);
            

            // make a prediction for a certain user and item
            //Console.WriteLine(_recommender.Predict(1, 1));

            //var bmf = new BiasedMatrixFactorization { Ratings = training_data };
            //Console.WriteLine(bmf.DoCrossValidation());
        }
    }
}
