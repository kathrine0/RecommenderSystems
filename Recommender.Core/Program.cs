using MyMediaLite.Eval;
using MyMediaLite.IO;
using MyMediaLite.RatingPrediction;
using Recommender.Core.MachineLearning;
using Recommender.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            // load the data
            //var training_data = RatingData.Read("u1.base");
            //var test_data = RatingData.Read("u1.test");

            var service = new RatingService();

            Console.WriteLine("Loading train set");
            var training_data = service.GetLearningSetMediaLiteFormat();

            Console.WriteLine("Loading test set");
            var test_data = service.GetTestSetMediaLiteFormat();

            // set up the recommender
            var recommender = new MatrixFactorization();
            recommender.Ratings = training_data;
            recommender.Train();

            var recommender2 = new C45Recommender();
            recommender.Ratings = training_data;
            recommender.Train();

            // measure the accuracy on the test data set
            var results = recommender.Evaluate(test_data);
            Console.WriteLine("RMSE={0} MAE={1}", results["RMSE"], results["MAE"]);
            Console.WriteLine(results);

            // make a prediction for a certain user and item
            Console.WriteLine(recommender.Predict(1, 1));

            var bmf = new BiasedMatrixFactorization { Ratings = training_data };
            Console.WriteLine(bmf.DoCrossValidation());
        }
    }
}
