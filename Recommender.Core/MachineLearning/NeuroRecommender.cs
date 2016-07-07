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
using Recommender.Common.Logger;
using System.Text;

namespace Recommender.Core.MachineLearning
{ 
    //TODO make it handle more ppl at once
    public class NeuroRecommender : RatingPredictor, IFeaturedPredictor, ILoggable
    {
        ///Stores neural networks per every user
        private IDictionary<int, ActivationNetwork> _networks;
        private IActivationFunction _activationFunction;
        private IDictionary<int, NeuralData> _neuralData;


        public Logger Logger { get; set; }

        //neural network parameters
        private int _iterationLimit = 1000;
        private TeacherFunction _teacherFunction = TeacherFunction.BackProp;
        private double _learningRate = 0.1;
        private double _momentum = 0.0;
        private double _sigmoidAlphaValue = 2.0;
        private double _learningErrorLimit = 0.1;
        private int _populationSize = 100;
        private int _hiddenLayerNeurons = 1;
        private bool _needToStop = false;
        private int _minimumRepeatingFeatures = 2;
        protected IFeaturedRatings _featuredRatings;

        public int IterationLimit
        {
            get { return _iterationLimit; }
            set { _iterationLimit = value; }
        }
        public TeacherFunction TeacherFunction
        {
            get { return _teacherFunction; }
            set { _teacherFunction = value; }
        }
        public bool RecommenderStatus { get; set; }
        public double LearningRate {
            get { return _learningRate; }
            set { _learningRate = value; }
        }
        public double Momentum {
            get { return _momentum; }
            set { _momentum = value; }
        }
        public double SigmoidAlphaValue {
            get { return _sigmoidAlphaValue; }
            set { _sigmoidAlphaValue = value;  }
        }
        public double LearningErrorLimit {
            get { return _learningErrorLimit; }
            set { _learningErrorLimit = value; }
        }
        public int PopulationSize
        {
            get
            {
                return _populationSize;
            }

            set
            {
                _populationSize = value;
            }
        }
        public int HiddenLayerNeurons {
            get { return _hiddenLayerNeurons; }
            set { _hiddenLayerNeurons = value; }
        }
        public int MinimumRepeatingFeatures
        {
            get { return _minimumRepeatingFeatures; }
            set { _minimumRepeatingFeatures = value; }
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
        public ActivationFunction ActivationFunctionType { get; set; }


        public NeuroRecommender()
        {
            RecommenderStatus = true;
        }

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

        public void LogTrainining()
        {
            var message = new StringBuilder();
            message.AppendFormat(@"
Training network with parameters:
    Iteration limit:            {0}
    Sigmoidal Alpha Value:      {1}
    Hidden Layer Neurons:       {2}
    Minimum Repeating Features: {3}
", IterationLimit, SigmoidAlphaValue, HiddenLayerNeurons, MinimumRepeatingFeatures);

            switch(TeacherFunction)
            {
                case TeacherFunction.BackProp:
                    message.AppendFormat(@"
    Teacher:                    Backpropagation
    LearningRate:               {0}
    Momentum:                   {1}", LearningRate, Momentum);
                    break;
                case TeacherFunction.Genetic:
                    message.AppendFormat(@"
    Teacher:                    Genetic Algorithm
    Population size:            {0} ", PopulationSize);
                    break;
                case TeacherFunction.ResilientBackProp:
                    message.AppendFormat(@"
    Teacher:                    Resilient backpropagation");
                    break;
            }


            Logger.AddProgressReport(new ProgressState(0, message.ToString(), null));
        }

       
        public override void Train()
        {
            LogTrainining();

            //set activation function
            SetActivationFunction();

            InitModel();//ratings.RandomIndex

            if (!RecommenderStatus) return;

            TrainNetworks();
        }

        private void SetActivationFunction()
        {
            _activationFunction = new SigmoidFunction(_sigmoidAlphaValue);
        }

        protected internal virtual void InitModel()
        {
            Logger.AddProgressReport(new ProgressState(1, null, "Initializing model..."));

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

                if (neuralData.Input[0].Length == 0)
                {
                    Logger.AddErrorReport(new ErrorReport("Not enough features"));
                    RecommenderStatus = false;
                    return;
                }


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

            double count = _neuralData.Count();
            double progressStep = 80 /count;
            //od 10 do 90 --- 80 jednostek

            Logger.AddProgressReport(new ProgressState(10, null, "Learning.."));

            Parallel.ForEach(_neuralData, data =>
            {
                //todo add bias --> Average

                var input = data.Value.Input;
                var output = data.Value.Output;

                if (input.Length == 0)
                    throw new ArgumentOutOfRangeException("No input");

                var network = new ActivationNetwork(_activationFunction, input[0].Length, _hiddenLayerNeurons, 1);

                ISupervisedLearning teacher;

                if (_teacherFunction == TeacherFunction.BackProp)
                {
                    teacher = new BackPropagationLearning(network);
                    ((BackPropagationLearning)teacher).LearningRate = _learningRate;
                    ((BackPropagationLearning)teacher).Momentum = _momentum;
                }
                else if (_teacherFunction == TeacherFunction.Genetic)
                    teacher = new EvolutionaryLearning(network, _populationSize);
                else if (_teacherFunction == TeacherFunction.ResilientBackProp)
                    teacher = new ResilientBackpropagationLearning(network);
                else
                    throw new ArgumentNullException("Unknown teacher");

                //ArrayList errorsList = new ArrayList();
                int iteration = 1;
                double error = 0;
 
                while (!_needToStop)
                {
                    error = teacher.RunEpoch(input, output);
                    //errorsList.Add(error);

                    // show current iteration & error
                    //currentIterationBox.Text = iteration.ToString( );
                    //currentErrorBox.Text = error.ToString( );

                    iteration++;

                    //Console.WriteLine(data.Key + " it: " + iteration + " err: " + error.ToString());

                    // check if we need to stop
                    if (error <= LearningErrorLimit || (_iterationLimit != 0 && iteration >= _iterationLimit))
                        break;
                }

                Console.WriteLine(data.Key + " error: " + error.ToString() + " iterations:" + iteration.ToString());

                _networks.Add(data.Key, network);

                Logger.IncrementProgress(progressStep);

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
}
