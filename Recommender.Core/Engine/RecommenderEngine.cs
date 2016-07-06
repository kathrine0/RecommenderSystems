using MyMediaLite.Data;
using MyMediaLite.Eval;
using MyMediaLite.RatingPrediction;
using Recommender.Common.Logger;
using Recommender.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender.Core.Engine
{
    /// <summary>
    /// This is the main application class
    /// </summary>
    public class RecommenderEngine
    {

        protected IRatingService _service;
        protected IRatings _trainingData;
        protected IRatings _testData;
        protected IRatingPredictor _recommender;

        public IRatings TrainingData
        {
            get { return _trainingData; }
            protected set { _trainingData = value; }
        }
        public IRatings TestData {
            get { return _testData; }
            protected set { _testData = value; }
        }
        public double TrainingSetRatio { get; protected set; }
        [Range(1, int.MaxValue)]
        public int NumberOfUsers { get; set; }
        [Range(1, int.MaxValue)]
        public int MinimumItemsRated { get; set; }
        public virtual IRatingPredictor Recommender
        {
            get { return _recommender; }
            set { _recommender = value; }
        }
        public bool DataLoaded { get; protected set; }
        public Logger Logger { get; protected set; }


        public RecommenderEngine(Logger logger)
        {
            Logger = logger;
            _service = new MovieLenseService();

            DataLoaded = false;
            NumberOfUsers = 1;
            TrainingSetRatio = 0.8;
        }

        public RecommenderEngine(RecommenderEngine engine)
        {
            //create shallow copy
            _service = new MovieLenseService();

            TrainingData = engine.TrainingData;
            TestData = engine.TestData;
            TrainingSetRatio = engine.TrainingSetRatio;
            NumberOfUsers = engine.NumberOfUsers;
            MinimumItemsRated = engine.MinimumItemsRated;
            DataLoaded = engine.DataLoaded;
            Logger = engine.Logger;

            if (engine.Recommender != null)
                Recommender = engine.Recommender;
        }

        public void SetTrainingSetRatio(decimal value)
        {
            if (value < 1 || value > 90)
                throw new ArgumentOutOfRangeException();

            TrainingSetRatio = (double)value / 100;
        }

        public void LoadData()
        {
            var reportText = string.Format(@"
Loading data:
    Number of users: {0} 
    Minimum rated items: {1}
    Ratio: {2}/{3}", NumberOfUsers, MinimumItemsRated, TrainingSetRatio * 100, 100 - TrainingSetRatio * 100);

            Logger.Logs.Add(new LogItem(LogType.ProgressReport, new ProgressState(0, reportText, "Loading data...")));

            //PrepareSets();
            //for now - always load featured sets. Will be easier to handle
            _service.LoadFeaturedData(out _trainingData, out _testData, TrainingSetRatio, NumberOfUsers, MinimumItemsRated);

            Logger.Logs.Add(new LogItem(LogType.ProgressReport, new ProgressState(90, null, null)));

            DataLoaded = true;

            Logger.Logs.Add(new LogItem(LogType.ProgressReport, new ProgressState(100, "Data Loaded", "Finished...")));
        }

        //public abstract void PrepareSets();

        public bool TeachRecommender()
        {
            if (!DataLoaded)
            {
                Logger.AddWarningReport(new WarningReport("No data loaded"));
                return false;
            }

            Recommender.Ratings = TrainingData;
            if (Recommender == null)
            {
                Logger.AddWarningReport(new WarningReport("Recommender not set"));
                return false;
            }

            // set up the recommender
            
            Recommender.Train();

            return true;
        }

        public RatingPredictionEvaluationResults GetResults()
        {
            
            // measure the accuracy on the test data set
            return Recommender.Evaluate(TestData);
            
            // make a prediction for a certain user and item
            //Console.WriteLine(_recommender.Predict(1, 1));

            //var bmf = new BiasedMatrixFactorization { Ratings = training_data };
            //Console.WriteLine(bmf.DoCrossValidation());
        }
    }
}
