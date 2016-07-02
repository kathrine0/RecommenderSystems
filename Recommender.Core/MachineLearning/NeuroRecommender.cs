using MyMediaLite.RatingPrediction;
using System;
using MyMediaLite.Data;
using Recommender.Service.Data;
using System.Linq;
using System.Collections.Generic;
using Accord.Math;
using Recommender.Common;
using AForge.Neuro;
using AForge.Neuro.Learning;
using System.Collections;

namespace Recommender.Core.MachineLearning
{
    //TODO make it handle more ppl at once
    public class NeuroRecommender : RatingPredictor, IFeaturedPredictor
    {

        private ActivationNetwork _network;
        private BackPropagationLearning _teacher;

        //list with all of the rated features
        private List<IFeature> allFeatures = new List<IFeature>();

        public NeuroRecommender()
        {
        }


        public virtual IFeaturedRatings FeaturedRatings
        {
            get { return featured_ratings; }
            set
            {
                featured_ratings = value;
                Ratings = value;
            }
        }
        /// <summary>rating data, including item features</summary>
        protected IFeaturedRatings featured_ratings;

        ///
        public override IRatings Ratings
        {
            get { return ratings; }
            set
            {
                if (!(value is IFeaturedRatings))
                    throw new ArgumentException("Ratings must be of type IFeaturedRatings.");

                base.Ratings = value;
                featured_ratings = (IFeaturedRatings)value;
            }
        }

        protected double weight;


        public override float Predict(int user_id, int item_id)
        {            
            throw new NotImplementedException();
        }

        public float Predict(int user_id, int item_id, IList<IFeature> features)
        {
            var inputList = new List<double[]>();

            var tmp = new List<double>();

            allFeatures
                .ForEach(d => tmp.Add(features
                                .Any(c => c.Name == d.Name && c.FeatureCategory == d.FeatureCategory) ?
                                1 : 0));

            var result = _network.Compute(tmp.ToArray());

            return Convert.ToSingle(result[0]);
        }

        public override void Train()
        {
            InitModel();//ratings.RandomIndex

            //so badly need to refactor
        }

        private double learningRate = 0.1;
        private double momentum = 0.0;
        private double sigmoidAlphaValue = 2.0;
        private double learningErrorLimit = 0.1;     
        private bool needToStop = false;


        protected internal virtual void InitModel()//IList<int> rating_indices
        {
            //please please please refactor me!
            //move it to featuredRatings class
            foreach (var userId in featured_ratings.AllUsers)
            {
                var userRatings = featured_ratings.ByUser[userId];
                //build up the items profiles with features
                if (userRatings.Count == 0)
                    continue;

                var ratedItems = new List<RatedItem>();

                //and items to datatable
                foreach (var index in userRatings)
                {
                    var features = featured_ratings.Features[index];

                    var itemId = featured_ratings.Items[index];

                    var x = featured_ratings[index];
                    ratedItems.Add(new RatedItem(itemId, userId, featured_ratings[index], features));

                    //todo make linq extensions out of it
                    features.Where(d => !allFeatures
                    .Any(c => c.Name == d.Name && c.FeatureCategory == d.FeatureCategory))
                    .ToList()
                    .ForEach(f => allFeatures.Add(f));
                }


                //prepare input and output
                //neural network
                // prepare learning data
 
                //todo add bias --> Average

                var outputList = new List<double[]>();
                var inputList = new List<double[]>();
                foreach(var ratedItem in ratedItems)
                {
                    var tmp = new List<double>();

                    allFeatures
                        .ForEach(d => tmp.Add(ratedItem.Features
                                        .Any(c => c.Name == d.Name && c.FeatureCategory == d.FeatureCategory) ?
                                        1 : 0));

                    inputList.Add(tmp.ToArray());
                    //todo change this 5 to some var!!
                    outputList.Add(new double[] { ratedItem.Rating / 5 });
                }

                var input = inputList.ToArray();
                var output = outputList.ToArray();

                // ... preparing the data ...

                // create perceptron

                _network = new ActivationNetwork( new SigmoidFunction(sigmoidAlphaValue), allFeatures.Count(), 1);
                //        (IActivationFunction)new BipolarSigmoidFunction(sigmoidAlphaValue),

                //ActivationNetwork network = new ActivationNetwork(new ThresholdFunction(), 2, classesCount);
                // create teacher
                //PerceptronLearning teacher = new PerceptronLearning(network);
                _teacher = new BackPropagationLearning(_network);
                // set learning rate and momentum
                _teacher.LearningRate = learningRate;
                _teacher.Momentum = momentum;

                // iterations
                int iteration = 1;

                ArrayList errorsList = new ArrayList();

                // loop
                while (!needToStop)
                {
                    // run epoch of learning procedure
                    double error = _teacher.RunEpoch(input, output);
                    errorsList.Add(error);

                    // show current iteration & error
                    //currentIterationBox.Text = iteration.ToString( );
                    //currentErrorBox.Text = error.ToString( );
                    iteration++;

                    // check if we need to stop
                    if (error <= learningErrorLimit)
                        break;
                }

                // show error's dynamics
                double[,] errors = new double[errorsList.Count, 2];

                for (int i = 0, n = errorsList.Count; i < n; i++)
                {
                    errors[i, 0] = i;
                    errors[i, 1] = (double)errorsList[i];
                }
            }

            if (FeaturedRatings.Features.Count < 1)
                throw new ArgumentOutOfRangeException("Not enough features");

            
        }
    }
}
