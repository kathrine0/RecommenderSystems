using MyMediaLite.Data;
using MyMediaLite.Eval;
using MyMediaLite.RatingPrediction;
using Recommender.Common.Enums;
using Recommender.Common.Logger;
using Recommender.Service;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;

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

        public IRatingService Service {
            get { return _service; }
            protected set { _service = value; }
        }

        public double TrainingSetRatio { get; protected set; }
        [Range(1, int.MaxValue)]
        public int BasicDataUsersQuantity { get; set; }

        [Range(1, int.MaxValue)]
        public int FeaturedDataUsersQuantity { get; set; }

        [Range(1, int.MaxValue)]
        public int MinimumItemsRated { get; set; }
        public virtual IRatingPredictor Recommender
        {
            get { return _recommender; }
            set {
                _recommender = value;
                AssignLogger(_recommender);
            }
        }
        public bool DataLoaded { get; protected set; }
        public Logger Logger { get; protected set; }
        public DataSetType DataSet { get; set; }

        public RecommenderEngine(Logger logger)
        {
            Logger = logger;
            _service = new MovieLenseService();
            _service.Logger = Logger;

            DataLoaded = false;
            BasicDataUsersQuantity = 1;
            TrainingSetRatio = 0.8;
        }

        public RecommenderEngine(RecommenderEngine engine)
        {
            //create shallow copy
           
            TrainingData = engine.TrainingData;
            TestData = engine.TestData;
            TrainingSetRatio = engine.TrainingSetRatio;
            BasicDataUsersQuantity = engine.BasicDataUsersQuantity;
            MinimumItemsRated = engine.MinimumItemsRated;
            DataLoaded = engine.DataLoaded;
            Logger = engine.Logger;

            if (engine.Service != null)
                Service = engine.Service;

            if (engine.Recommender != null)
                Recommender = engine.Recommender;
        }

        public void SetTrainingSetRatio(decimal value)
        {
            if (value < 1 || value > 90)
                throw new ArgumentOutOfRangeException();

            TrainingSetRatio = (double)value / 100;
        }

        public virtual void LoadData(CancellationToken token)
        {
            PrepareSets(token);
            
            Logger.AddProgressReport(new ProgressState(90, null, null));

            Recommender.Ratings = TrainingData;
            DataLoaded = true;

            Logger.AddProgressReport(new ProgressState(100, "Data Loaded", "Finished..."));
        }

        public abstract void PrepareSets(CancellationToken token);

        public virtual bool TeachRecommender()
        {
            if (_service == null)
            {
                Logger.AddErrorReport(new ErrorReport("Dataset not set"));
                return false;
            }

            if (!DataLoaded)
            {
                Logger.AddWarningReport(new WarningReport("No data loaded"));
                return false;
            }
            
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

        public virtual RatingPredictionEvaluationResults GetResults()
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

        public void SetDataSet(DataSetType dataset)
        {
            switch (dataset)
            {
                case DataSetType.AmazonMeta:
                    _service = new AmazonMetaService();
                    break;
                case DataSetType.MovieLense:
                    _service = new MovieLenseService();
                    break;
                case DataSetType.YahooMusic:
                    _service = new YahooMusicService();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Invalid dataset option");
            }

            _service.Logger = Logger;
        }

        protected void AssignLogger(IRatingPredictor recommender)
        {
            if (recommender is ILoggable)
                ((ILoggable)recommender).Logger = Logger;
        }
    }
}
