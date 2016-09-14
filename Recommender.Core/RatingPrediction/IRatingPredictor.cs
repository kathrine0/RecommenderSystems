namespace Recommender.Core.RatingPrediction
{
    public interface IRatingPredictor : MyMediaLite.RatingPrediction.IRatingPredictor
    {
        void LogTrainining();
    }
}
