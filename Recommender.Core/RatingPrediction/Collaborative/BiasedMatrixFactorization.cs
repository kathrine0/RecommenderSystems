
using System;
using Recommender.Common.Logger;
using System.Text;

namespace Recommender.Core.RatingPrediction.Collaborative
{
    public class BiasedMatrixFactorization : MyMediaLite.RatingPrediction.BiasedMatrixFactorization, IRatingPredictor, ILoggable
    {
        public Logger Logger { get; set; }

        public bool RecommenderStatus { get; set; }

        public BiasedMatrixFactorization() : base()
        {
            RecommenderStatus = true;
        }
        public override void Train()
        {
            MaxThreads = 10;            
            base.Train();
        }

        public void LogTrainining()
        {
            var message = new StringBuilder();
            message.AppendFormat(@"
    Biased Matrix Factorization with parameters:
        Regularization:            {0}
        LearnRate:                 {1}
        Decay:                     {2}
        NumIter:                   {3}
        InitStdDev:                {4}
        NumFactors:                {5}
        BiasReg:                   {6}
        BiasLearnRate:             {7}
        MaxThreads:                {8}
    ", Regularization, LearnRate, Decay, NumIter, InitStdDev, NumFactors, BiasReg, BiasLearnRate, MaxThreads);

            Logger.AddProgressReport(new ProgressState(0, message.ToString(), "Learning.."));
        }
    }

}
