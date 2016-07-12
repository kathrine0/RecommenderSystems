﻿using MyMediaLite.Data;
using MyMediaLite.Eval;
using MyMediaLite.RatingPrediction;
using Recommender.Common.Logger;
using Recommender.Core.MachineLearning;
using Recommender.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Recommender.Core.Engine
{
    /// <summary>
    /// This is the main application class
    /// </summary>
    public abstract class RecommenderEngine
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

        public void LoadData(CancellationToken token)
        {
            var reportText = string.Format(@"
Loading data:
    Number of users: {0} 
    Minimum rated items: {1}
    Ratio: {2}/{3}
", NumberOfUsers, MinimumItemsRated, TrainingSetRatio * 100, 100 - TrainingSetRatio * 100);


            Logger.AddProgressReport(new ProgressState(0, reportText, "Loading data..."));

            PrepareSets(token);
            
            Logger.AddProgressReport(new ProgressState(90, null, null));

            DataLoaded = true;

            Logger.AddProgressReport(new ProgressState(100, "Data Loaded", "Finished..."));
        }

        public abstract void PrepareSets(CancellationToken token);

        public virtual bool TeachRecommender()
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


            //if recommender supports logs:
            if(Recommender is ILoggable)
                return ((ILoggable)Recommender).RecommenderStatus;
            else
                return true;
        }

        public RatingPredictionEvaluationResults GetResults()
        {
            Logger.AddProgressReport(new ProgressState(90, "", "Evaluating results..."));
            // measure the accuracy on the test data set
            var result = Recommender.Evaluate(TestData);

            var resString = result.ToString() + "\n =========================================";

            Logger.AddProgressReport(new ProgressState(100, resString, "Finished"));

            return result;
            // make a prediction for a certain user and item
            //Console.WriteLine(_recommender.Predict(1, 1));

            //var bmf = new BiasedMatrixFactorization { Ratings = training_data };
            //Console.WriteLine(bmf.DoCrossValidation());
        }
    }
}
