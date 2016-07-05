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
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Recommender.Core.MachineLearning
{
    class NeuralData
    {
        public NeuralData(double[][] input, double[][] output, IList<IFeature> allFeatures)
        {
            Input = input;
            Output = output;
            AllFeatures = allFeatures;
        }

        public double[][] Input { get; private set; }
        public double[][] Output { get; private set; }

        public IList<IFeature> AllFeatures { get; private set; }

    }


    //TODO make it handle more ppl at once
    public class NeuroRecommender : RatingPredictor, IFeaturedPredictor
    {
        ///Stores neural networks per every user
        private IDictionary<int, ActivationNetwork> _networks;

        private IActivationFunction _activationFunction;

        private IDictionary<int, NeuralData> _neuralData;

        //neural network parameters
        private int _iterationLimit = 10000;
        public int IterationLimit
        {
            get { return _iterationLimit; }
            set { _iterationLimit = value; }
        }


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
            get { return _featuredRatings; }
            set
            {
                _featuredRatings = value;
                Ratings = value;
            }
        }
        /// <summary>rating data, including item features</summary>
        protected IFeaturedRatings _featuredRatings;

        ///
        public override IRatings Ratings
        {
            get { return ratings; }
            set
            {
                if (!(value is IFeaturedRatings))
                    throw new ArgumentException("Ratings must be of type IFeaturedRatings.");

                base.Ratings = value;
                _featuredRatings = (IFeaturedRatings)value;
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

            _neuralData[user_id].AllFeatures.ToList()
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
            _neuralData = new Dictionary<int, NeuralData>();

            if (FeaturedRatings.Features.Count < 1)
                throw new ArgumentOutOfRangeException("Not enough features");

            foreach (var userId in _featuredRatings.AllUsers)
            {
                var userRatings = _featuredRatings.ByUser[userId];
                //build up the items profiles with features
                if (userRatings.Count == 0)
                    continue;

                var ratedItems = ParseRatedItems(userRatings);
                var allFeatures = FetchDistinctFeatures(ratedItems);
                var neuralData = BuildNeuralData(ratedItems, allFeatures);

                _neuralData.Add(userId, neuralData);
            }
        }

        private IList<IFeature> FetchDistinctFeatures(IList<RatedItem> ratedItems)
        {
            var tmpFeatureStorage = ratedItems.SelectMany(x => x.Features).ToList();

            var distinctFeatures = tmpFeatureStorage.GroupBy(x => new { x.Name, x.FeatureCategory })
                                            .Where(x => x.Count() >= _minimumRepeatingFeatures)
                                            .Select(x => x.First()).ToList();

            return distinctFeatures;
        }

        private IList<RatedItem> ParseRatedItems(IList<int> userRatings)
        {
            var ratedItems = new List<RatedItem>();

            foreach (var index in userRatings)
            {
                var features = _featuredRatings.Features[index];
                var rating = _featuredRatings[index];

                ratedItems.Add(new RatedItem(rating, features));
            }

            return ratedItems;
        }

        private NeuralData BuildNeuralData(IList<RatedItem> ratedItems, IList<IFeature> allFeatures)
        {
            var outputList = new List<double[]>();
            var inputList = new List<double[]>();

            //build input and output for neural network
            foreach (var ratedItem in ratedItems)
            {
                var tmp = new List<double>();

                allFeatures.ToList().ForEach(d => tmp.Add(ratedItem.Features
                                    .Any(c => c.Name == d.Name && c.FeatureCategory == d.FeatureCategory) ?
                                    1 : 0));

                inputList.Add(tmp.ToArray());
                //todo verify if this is good approach
                outputList.Add(new double[] { ratedItem.Rating / MaxRating });
            }

            return new NeuralData(inputList.ToArray(), outputList.ToArray(), allFeatures);
        }

        private void TrainNetworks()
        {
            _networks = new ConcurrentDictionary<int, ActivationNetwork>();

            //    public static ConcurrentBag<SearchResult> Search(string title)
            //{
            //    var results = new ConcurrentBag<SearchResult>();
            //    Parallel.ForEach(Providers, currentProvider =>
            //    {
            //        results.Add(currentProvider.SearchTitle((title)));
            //    });

            //    return results;
            //}

            Parallel.ForEach(_neuralData, data =>
            {
                //todo add bias --> Average

                var input = data.Value.Input;
                var output = data.Value.Output;

                if (input.Length == 0)
                    throw new ArgumentOutOfRangeException("No input");

                var network = new ActivationNetwork(_activationFunction, input[0].Length, _hiddenLayerNeurons, 1);
                var teacher = new BackPropagationLearning(network);

                teacher.LearningRate = _learningRate;
                teacher.Momentum = _momentum;

                //ArrayList errorsList = new ArrayList();
                int iteration = 1;
                // loop
                while (!needToStop)
                {
                    double error = teacher.RunEpoch(input, output);
                    //errorsList.Add(error);

                    // show current iteration & error
                    //currentIterationBox.Text = iteration.ToString( );
                    //currentErrorBox.Text = error.ToString( );

                    iteration++;

                    Console.WriteLine(data.Key + "it: " + iteration + "err: " + error.ToString());

                    // check if we need to stop
                    if (error <= LearningErrorLimit || (_iterationLimit != 0 && iteration > _iterationLimit))
                        break;
                }

                _networks.Add(data.Key, network);

                // show error's dynamics
                //double[,] errors = new double[errorsList.Count, 2];

                //for (int i = 0, n = errorsList.Count; i < n; i++)
                //{
                //    errors[i, 0] = i;
                //    errors[i, 1] = (double)errorsList[i];
                //}
            });
        }

    }
}
