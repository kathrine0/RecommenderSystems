using MyMediaLite.Eval;
using MyMediaLite.RatingPrediction;
using Recommender.Common.Enums;
using Recommender.Common.Logger;
using Recommender.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Linq;
using Recommender.Service.Data;
using MyMediaLite.Data;

namespace Recommender.Core.Engine
{
    /// <summary>
    /// This is the main application class
    /// </summary>
    public abstract class RecommenderEngine
    {

        protected IRatingService _service;
        protected IRatings _data;
        protected IFeaturedRatings _featuredData;
        protected RatingPrediction.IRatingPredictor _recommender;

        public IRatings Data
        {
            get { return _data; }
            protected set { _data = value; }
        }

        public IFeaturedRatings FeaturedData
        {
            get { return _featuredData; }
            protected set { _featuredData = value; }
        }

        public IRatingService Service {
            get { return _service; }
            protected set { _service = value; }
        }

        [Range(1, 10)]
        public uint Crossvalidation { get; set; }
        [Range(1, int.MaxValue)]
        public int BasicDataUsersQuantity { get; set; }

        [Range(1, int.MaxValue)]
        public int FeaturedDataUsersQuantity { get; set; }

        [Range(1, int.MaxValue)]
        public int MinimumItemsRated { get; set; }
        public virtual RatingPrediction.IRatingPredictor Recommender
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
            Crossvalidation = 5;
        }

        public RecommenderEngine(RecommenderEngine engine)
        {
            //create shallow copy
           
            Data = engine.Data;
            Crossvalidation = engine.Crossvalidation;
            BasicDataUsersQuantity = engine.BasicDataUsersQuantity;
            MinimumItemsRated = engine.MinimumItemsRated;
            DataLoaded = engine.DataLoaded;
            Logger = engine.Logger;

            if (engine.Service != null)
                Service = engine.Service;

            if (engine.Recommender != null)
                Recommender = engine.Recommender;
        }

        public virtual Dictionary<string, float> TestRecommender(CancellationToken token)
        {
            var result = new Dictionary<string, float>() {
                { "RMSE", 0 },
                { "MAE", 0 },
                { "CBD", 0 }
            };

            RatingCrossValidationSplit split;

            if (_data != null)
                split = new Engine.RatingCrossValidationSplit(_data, Crossvalidation);
            else
                split = new Engine.RatingCrossValidationSplit(_featuredData, Crossvalidation);

            int i = 0;

            Recommender.LogTrainining();

            for (i = 0; i < split.NumberOfFolds; i++)
            {
                if (token.IsCancellationRequested)
                    throw new OperationCanceledException(token);

                Logger.AddProgressReport(new ProgressState(40, null, "Learning: fold " + (i+1)));

                Recommender.Ratings = split.Train[i];

                var teachingResult = TeachRecommender();

                if (teachingResult)
                {
                    IncrementResults(result, Recommender.Evaluate(split.Test[i]));
                }
            }

            AdjustResults(result, i);
            PublishResults(result);

            return result;
        }

        protected void IncrementResults(Dictionary<string, float> result, RatingPredictionEvaluationResults partial_result)
        {
            result["RMSE"] += partial_result["RMSE"];
            result["MAE"] += partial_result["MAE"];
            result["CBD"] += partial_result["CBD"];
        }

        protected void AdjustResults(Dictionary<string, float> result, int iters)
        {
            result["RMSE"] = result["RMSE"] / iters;
            result["MAE"] = result["MAE"] / iters;
            result["CBD"] = result["CBD"] / iters;
        }

        public virtual void LoadData(CancellationToken token)
        {
            PrepareSets(token);
            
            Logger.AddProgressReport(new ProgressState(90, null, null));
            
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

        public void PublishResults(Dictionary<string, float> result)
        {
            Logger.AddProgressReport(new ProgressState(90, "", "Evaluating results..."));
            var resString = new StringBuilder("RMSE \t MAE \t CBD \n");
            resString.AppendLine(string.Format("{0} \t {1} \t {2} \n", result["RMSE"], result["MAE"], result["CBD"]));

            resString.AppendLine("------------------------------");

            Logger.AddProgressReport(new ProgressState(100, resString.ToString(), "Finished"));
        }

        public void SetDataSet(DataSetType dataset)
        {
            StringBuilder reportText = new StringBuilder("DataSet: ");

            switch (dataset)
            {
                case DataSetType.AmazonMeta:
                    _service = new AmazonMetaService();
                    reportText.Append("AmazonMeta");
                    break;
                case DataSetType.MovieLense:
                    _service = new MovieLenseService();
                    reportText.Append("MovieLense");
                    break;
                case DataSetType.YahooMusic:
                    _service = new YahooMusicService();
                    reportText.Append("YahooMusic");
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Invalid dataset option");
            }

            _service.Logger = Logger;
            Logger.AddProgressReport(new ProgressState(0, reportText.ToString(), null));
        }

        protected void AssignLogger(IRatingPredictor recommender)
        {
            if (recommender is ILoggable)
                ((ILoggable)recommender).Logger = Logger;
        }
    }
}
