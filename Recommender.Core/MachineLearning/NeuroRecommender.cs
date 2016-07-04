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
using Recommender.Core.Enums;

namespace Recommender.Core.MachineLearning
{
    //TODO make it handle more ppl at once
    public class NeuroRecommender : RatingPredictor, IFeaturedPredictor
    {
        ///Stores neural networks per every user
        private IDictionary<int, ActivationNetwork> _networks;

        private IActivationFunction _activationFunction;
        //list with all of the rated features per user
        private IDictionary<int, IList<IFeature>> _allFeatures;
        //list with all of the rated features per user
        private IDictionary<int, IList<RatedItem>> _ratedItems;

        //neural network parameters
        private double _learningRate = 0.1;
        public double LearningRate {
            get { return _learningRate; }
            set { _learningRate = value; }
        }

        private double _momentum = 0.0;
        public double Momentum {
            get { return _momentum; }
            set { _momentum = value; }
        }

        private double _sigmoidAlphaValue = 2.0;
        public double SigmoidAlphaValue {
            get { return _sigmoidAlphaValue; }
            set { _sigmoidAlphaValue = value;  }
        }

        private double _learningErrorLimit = 0.1;
        public double LearningErrorLimit {
            get { return _learningErrorLimit; }
            set { _learningErrorLimit = value; }
        }

        private int _hiddenLayerNeurons = 1;
        public int HiddenLayerNeurons {
            get { return _hiddenLayerNeurons; }
            set { _hiddenLayerNeurons = value; }
        }

        private bool needToStop = false;

        private int _minimumRepeatingFeatures = 2;

        public int MinimumRepeatingFeatures
        {
            get { return _minimumRepeatingFeatures = 2; }
            set { _minimumRepeatingFeatures = value; }
        }


        public ActivationFunction ActivationFunctionType { get; set; }

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

            _allFeatures[user_id].ToList()
                .ForEach(d => tmp.Add(features
                                .Any(c => c.Name == d.Name && c.FeatureCategory == d.FeatureCategory) ?
                                1 : 0));

            var result = _networks[user_id].Compute(tmp.ToArray());

            return Convert.ToSingle(result[0]*MaxRating);
        }

        public override void Train()
        {
            //set activation function
            SetActivationFunction();

            InitModel();//ratings.RandomIndex

            TrainNetworks();
        }

        private void SetActivationFunction()
        {
            _activationFunction = new SigmoidFunction(_sigmoidAlphaValue);
        }

        protected internal virtual void InitModel()//IList<int> rating_indices
        {
            _allFeatures = new Dictionary<int, IList<IFeature>>();
            _ratedItems = new Dictionary<int, IList<RatedItem>>();
            _networks = new Dictionary<int, ActivationNetwork>();

            if (FeaturedRatings.Features.Count < 1)
                throw new ArgumentOutOfRangeException("Not enough features");

            //please please please refactor me!
            //move it to featuredRatings class

            foreach (var userId in featured_ratings.AllUsers)
            {
                var userRatings = featured_ratings.ByUser[userId];
                //build up the items profiles with features
                if (userRatings.Count == 0)
                    continue;

                _ratedItems.Add(userId, new List<RatedItem>());

                var tmpFeatureStorage = new List<IFeature>();

                //and crete _allFeatures data  
                foreach (var index in userRatings)
                {
                    var features = featured_ratings.Features[index];
                    var itemId = featured_ratings.Items[index];
                    _ratedItems[userId].Add(new RatedItem(itemId, userId, featured_ratings[index], features));

                    tmpFeatureStorage.AddRange(features);

                    //todo make linq extensions out of it
                    //features.Where(d => !_allFeatures[userId]
                    //.Any(c => c.Name == d.Name && c.FeatureCategory == d.FeatureCategory))
                    //.ToList()
                    //.ForEach(f => _allFeatures[userId].Add(f));
                }

                var disctinctFeatures = tmpFeatureStorage.GroupBy(x => new { x.Name, x.FeatureCategory })
                                            .Where(x => x.Count() >= _minimumRepeatingFeatures)
                                            .Select(x => x.First()).ToList();

                _allFeatures.Add(userId, disctinctFeatures);
            }
        }

        private void TrainNetworks()
        {
            foreach (var userId in featured_ratings.AllUsers)
            {
                //prepare input and output
                //neural network
                // prepare learning data

                //todo add bias --> Average

                var outputList = new List<double[]>();
                var inputList = new List<double[]>();
                foreach (var ratedItem in _ratedItems[userId])
                {
                    var tmp = new List<double>();

                    _allFeatures[userId].ToList()
                        .ForEach(d => tmp.Add(ratedItem.Features
                                        .Any(c => c.Name == d.Name && c.FeatureCategory == d.FeatureCategory) ?
                                        1 : 0));

                    inputList.Add(tmp.ToArray());
                    //todo verify if this is good approach
                    outputList.Add(new double[] { ratedItem.Rating / MaxRating });
                }

                var input = inputList.ToArray();
                var output = outputList.ToArray();

                // ... preparing the data ...
                // create perceptron

                //TODO check what is neurons count parameters
                var network = new ActivationNetwork(_activationFunction, _allFeatures[userId].Count(), _hiddenLayerNeurons, 1);

                // create teacher
                var teacher = new BackPropagationLearning(network);

                // set learning rate and momentum
                teacher.LearningRate = _learningRate;
                teacher.Momentum = _momentum;

                ArrayList errorsList = new ArrayList();
                int iteration = 1;
                // loop
                while (!needToStop)
                {
                    // run epoch of learning procedure
                    double error = teacher.RunEpoch(input, output);
                    errorsList.Add(error);

                    // show current iteration & error
                    //currentIterationBox.Text = iteration.ToString( );
                    //currentErrorBox.Text = error.ToString( );

                    iteration++;

                    // check if we need to stop
                    if (error <= LearningErrorLimit)
                        break;
                }

                _networks.Add(userId, network);

                // show error's dynamics
                //double[,] errors = new double[errorsList.Count, 2];

                //for (int i = 0, n = errorsList.Count; i < n; i++)
                //{
                //    errors[i, 0] = i;
                //    errors[i, 1] = (double)errorsList[i];
                //}
            }
        }

    }
}
