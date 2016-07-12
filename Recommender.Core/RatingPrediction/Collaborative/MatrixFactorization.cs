using System;
using Recommender.Common.Logger;
using System.Text;

namespace Recommender.Core.RatingPrediction.Collaborative
{
    public class MatrixFactorization : MyMediaLite.RatingPrediction.MatrixFactorization, ILoggable
    {
        public Logger Logger { get; set; }

        public bool RecommenderStatus { get; set; }

        public MatrixFactorization() : base()
        {
            RecommenderStatus = true;
        }

        public override void Train()
        {
            LogTrainining();

            base.Train();
        }

        public void LogTrainining()
        {
            var message = new StringBuilder();
            message.AppendFormat(@"
Matrix Factorization with parameters:
    Regularization:            {0}
    LearnRate:                 {1}
    Decay:                     {2}
    NumIter:                   {3}
    InitStdDev:                {4}
    NumFactors:                {5}
", Regularization, LearnRate, Decay, NumIter, InitStdDev, NumFactors);


            Logger.AddProgressReport(new ProgressState(0, message.ToString(), null));
        }


    }
}
