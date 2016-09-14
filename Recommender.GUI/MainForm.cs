using Recommender.Common.Logger;
using Recommender.Core.Engine;
using Recommender.Core.Enums;
using Recommender.Core.RatingPrediction.ContentBased;
using Recommender.Core.RatingPrediction.Collaborative;
using Recommender.Common.Enums;
using Recommender.GUI.Extensions;
using Recommender.GUI.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Recommender.Core.RatingPrediction.Hybrid;
using IRatingPredictor = MyMediaLite.RatingPrediction.IRatingPredictor;
using System.Collections.Generic;
using System.Diagnostics;

namespace Recommender.GUI
{
    public partial class MainForm : Form
    {

        RecommenderEngine _recommenderEngine;
        Logger _logger;
        CancellationTokenSource _cts;

        public MainForm()
        {
            SetupLogger();
            InitializeComponent();
            InitializeCombos();            
        }

        private void SetupLogger()
        {
            _logger = new Logger();
            _logger.Logs.OnAdd += Logs_OnAdd;
        }

        private void InitializeCombos()
        {
            CreateCombo(this.datasetCombo, DataSetOption.OptionBuilder());
            CreateCombo(this.RecommenderCombo, RecommenderTypeOption.OptionBuilder());
            CreateCombo(this.CollaborativeAlgorithmCombo, CollaborativeAlgorithmOption.OptionBuilder());
            CreateCombo(this.ContentBasedAlgorithmCombo, ContentBasedAlgorithmOption.OptionBuilder());
            CreateCombo(this.ContentBased_Teacher, TeacherFunctionOption.OptionBuilder());
        }

        private void CreateCombo(ComboBox combo, object options)
        {
            combo.DataSource = options;
            combo.DisplayMember = "Name";
            combo.ValueMember = "Value";
        }

        #region logger

        private void Logs_OnAdd(object sender, LogItemAddedEventArgs e)
        {
            var logItem = e.LogItem;

            switch (logItem.Type)
            {
                case LogType.ProgressReport:
                    ThreadSafeProgressReport(logItem.Value as ProgressState);
                    break;
                case LogType.IncrementProgress:
                    ThreadSafeIncrementProgress((double)logItem.Value);
                    break;
                case LogType.WarningReport:
                    ThreadSafeWarningReport(logItem.Value as WarningReport);
                    break;
                case LogType.ErrorReport:
                    ThreadSafeErrorReport(logItem.Value as ErrorReport);
                    break;
                case LogType.LoudReport:
                    ThreadSafeLoudReport(logItem.Value as LoudReport);
                    break;

            }
        }

        private double _progressStore = 0;
        private void ThreadSafeProgressReport(ProgressState progressState)
        {
            this.StatusStrip.PerformSafely(() =>
            {
                //update progressbar
                _progressStore = progressState.Percentage;
                this.ProgressBar.Value = progressState.Percentage;

                //update status text
                var statusText = this.StatusLabel.Text;

                if (!string.IsNullOrEmpty(progressState.StatusText))
                    statusText = progressState.StatusText;

                this.StatusLabel.Text = statusText;
                this.ProcentLabel.Text = ProgressBar.Value.ToString() + "%";

            });

            //update resultbox text
            if (!string.IsNullOrEmpty(progressState.ResultBoxText) && !_logger.SilentMode)
                this.ResultBox.PerformSafely(() => AddResultBoxText(progressState.ResultBoxText));

            if (_progressStore >= 100)
                _progressStore = 0;
        }

        private void ThreadSafeIncrementProgress(double progressStep)
        {
            _progressStore += progressStep;

            if (_progressStore >= 100)
                _progressStore = 0;

            this.StatusStrip.PerformSafely(() =>
            {
                this.ProgressBar.Value = (int)Math.Round(_progressStore);
                this.ProcentLabel.Text = ProgressBar.Value.ToString() + "%";
            });


        }

        private void ThreadSafeWarningReport(WarningReport warningReport)
        {
            MessageBox.Show(warningReport.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void ThreadSafeErrorReport(ErrorReport errorReport)
        {
            MessageBox.Show(errorReport.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            this.StatusStrip.PerformSafely(() =>
            {
                this.ProgressBar.Value = 0;
                this.ProcentLabel.Text = "";
                this.StatusLabel.Text = "Finished";
            });
        }

        private void ThreadSafeLoudReport(LoudReport report)
        {
            if (_logger.SilentMode)
                this.ResultBox.PerformSafely(() => AddResultBoxText(report.Message));
        }


        #endregion logger

        private void AddResultBoxText(string text)
        {
            this.ResultBox.AppendText(text + "\n");
        }

        private void SetupRecommenderEngine()
        {
            _recommenderEngine = ChooseRecommenderEngine();
            //data settings
            var dataset = (DataSetType) this.datasetCombo.SelectedValue;

            _recommenderEngine.BasicDataUsersQuantity = decimal.ToInt32(this.BasicDataUsersQuantity.Value);
            _recommenderEngine.FeaturedDataUsersQuantity = decimal.ToInt32(this.FeaturedDataUsersQuantity.Value);
            _recommenderEngine.MinimumItemsRated = decimal.ToInt32(this.MinimumItemsRated.Value);
            _recommenderEngine.Crossvalidation = decimal.ToUInt32(this.Crossvalidation.Value);
            _recommenderEngine.SetDataSet(dataset);
        }

        private RecommenderEngine ChooseRecommenderEngine()
        {
            RecommenderEngine re;

            switch ((RecommenderType)(this.RecommenderCombo.SelectedValue))
            {
                case RecommenderType.Collaborative:
                    re = new CollaborativeRecommenderEngine(_logger);
                    re.Recommender = ChooseCollaborativeAlgorithm();
                    break;
                case RecommenderType.ContentBased:
                    re = new ContentRecommenderEngine(_logger);
                    re.Recommender = ChooseContentBasedAlgorithm();
                    break;
                case RecommenderType.Hybrid:
                    re = new HybridRecommenderEngine(_logger);
                    re.Recommender = ChooseHybridAlgorithm();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Unknown recommender type");
            }

            return re;
        }

        private Core.RatingPrediction.IRatingPredictor ChooseCollaborativeAlgorithm()
        {
            switch ((CollaborativeAlgorithm)(this.CollaborativeAlgorithmCombo.SelectedValue))
            {
                case CollaborativeAlgorithm.MatrixFactorization:
                    return new MatrixFactorization();
                case CollaborativeAlgorithm.BiasedMatrixFactorization:
                    return new BiasedMatrixFactorization();
                case CollaborativeAlgorithm.SVDplusplus:
                    return new SVDPlusPlus();
                default:
                    throw new ArgumentOutOfRangeException("Unknown recommender type");
            }
        }

        private IFeaturedPredictor ChooseContentBasedAlgorithm()
        {
            var recommender = new NeuroRecommender();

            recommender.MinimumRepeatingFeatures = decimal.ToInt32(this.ContentBased_MinFeatures.Value);

            //neuro settings
            recommender.SigmoidAlphaValue = decimal.ToDouble(this.ContentBased_SigmoidAlpha.Value);
            recommender.HiddenLayerNeurons = decimal.ToInt32(this.ContentBased_HiddenLayerNeurons.Value);
            recommender.IterationLimit = decimal.ToInt32(this.ContentBased_Iterations.Value);

            //teacher settings
            recommender.Momentum = decimal.ToDouble(this.ContentBased_Momentum.Value);
            recommender.LearningRate = decimal.ToDouble(this.ContentBased_LearningRate.Value);
            recommender.PopulationSize = decimal.ToInt32(this.ContentBased_PopulationSize.Value);
            recommender.TeacherFunction = ((TeacherFunctionOption)ContentBased_Teacher.SelectedItem).Value;

            return recommender;
        }

        private IHybridPredictor ChooseHybridAlgorithm()
        {
            
            var recommender = new HybridRecommender();

            recommender.Logger = _logger;
            recommender.CollaborativeRecommender = ChooseCollaborativeAlgorithm();
            recommender.ContentRecommender = ChooseContentBasedAlgorithm();

            return recommender;
        }

        #region Long actions

        private void DoWorkAsync()
        {
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            Task.Run(() => DoWork(token));
        }

        private void DoWork(CancellationToken token)
        {
            ThreadSafeButtonToggle(false, true);

            try
            {
                _recommenderEngine.LoadData(token);
                _recommenderEngine.TestRecommender(token);
                
            }
            catch (OperationCanceledException)
            {
                _logger.AddProgressReport(new ProgressState(0, "Operation canceled", "Operation canceled"));
            }
            finally
            {
                ThreadSafeButtonToggle(true, false);
                _cts.Dispose();
            }
        }

        #endregion Long actions

        #region GUI Interactions

        private void RunButton_Click(object sender, EventArgs e)
        {
            SetupRecommenderEngine();
            DoWorkAsync();
        }

        private void recommenderCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = (RecommenderTypeOption)RecommenderCombo.SelectedItem;

            switch (selected.Value)
            {
                case RecommenderType.Collaborative:
                    this.CollaborativeAlgorithmChoice.Enabled = true;
                    this.ContentBasedAlghoritmChoice.Enabled  = false;

                    this.CollaborativeOptions.Visible = true;
                    this.ContentBasedOptions.Visible = false;
                    this.NeuralNetworkOptions.Visible = false;
                    break;
                case RecommenderType.ContentBased:
                    this.CollaborativeAlgorithmChoice.Enabled = false;
                    this.ContentBasedAlghoritmChoice.Enabled = true;

                    this.CollaborativeOptions.Visible = false;
                    this.ContentBasedOptions.Visible = true;
                    this.NeuralNetworkOptions.Visible = true;
                    break;
                case RecommenderType.Hybrid:
                    this.CollaborativeAlgorithmChoice.Enabled = true;
                    this.ContentBasedAlghoritmChoice.Enabled = true;

                    this.CollaborativeOptions.Visible = true;
                    this.ContentBasedOptions.Visible = true;
                    this.NeuralNetworkOptions.Visible = true;
                    break;
            }

        }

        private void ContentBased_Teacher_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = (TeacherFunctionOption)ContentBased_Teacher.SelectedItem;

            switch (selected.Value)
            {
                case TeacherFunction.BackProp:
                    this.ContentBased_BackPropPanel.Visible = true;
                    this.ContentBased_GeneticPanel.Visible = false;
                    break;
                case TeacherFunction.Genetic:
                    this.ContentBased_BackPropPanel.Visible = false;
                    this.ContentBased_GeneticPanel.Visible = true;
                    break;
                case TeacherFunction.ResilientBackProp:
                    this.ContentBased_BackPropPanel.Visible = false;
                    this.ContentBased_GeneticPanel.Visible = false;
                    break;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            _cts.Cancel();
            ThreadSafeButtonToggle(false, false);
        }

        private void ThreadSafeButtonToggle(bool runbtn, bool cancelbtn)
        {
            //this.LoadDataButton.PerformSafely(() => this.LoadDataButton.Enabled = loadbtn);
            this.RunButton.PerformSafely(() => this.RunButton.Enabled = runbtn);
            this.CancelButton.PerformSafely(() => this.CancelButton.Enabled = cancelbtn);
        }

        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ResultBox.Clear();
        }

        private void MagicButton_Click(object sender, EventArgs e)
        {
            //1) choose data set
            this.datasetCombo.SelectedValue = DataSetType.MovieLense;

            //2) choose alghoritm and settings
            MagicContentBased();

        }

        #endregion

        private void DoMagicAction(double i, string changableName, CancellationToken token)
        {
            try
            {
                var watch = Stopwatch.StartNew();
                var results = _recommenderEngine.TestRecommender(token);
                watch.Stop();

                _logger.AddLoudReport(new LoudReport(string.Format("{0} {1} {2} {3} {4}", i, results["RMSE"], results["MAE"], results["CBD"], watch.ElapsedMilliseconds)));

            }
            catch (OperationCanceledException)
            {
                _logger.AddProgressReport(new ProgressState(0, "Operation canceled", "Operation canceled"));
                _logger.AddLoudReport(new LoudReport("Operation canceled"));
            }
            finally
            {
                ThreadSafeButtonToggle(true, false);
                _cts.Dispose();               
            }
            
        }

        private void DoMagicAction2(double i, double j, CancellationToken token)
        {
            try
            {

                var results = _recommenderEngine.TestRecommender(token);
                _logger.AddLoudReport(new LoudReport(string.Format("{0} {1} {2} {3} {4}", i, j, results["RMSE"], results["MAE"], results["CBD"])));
                

            }
            catch (OperationCanceledException)
            {
                _logger.AddProgressReport(new ProgressState(0, "Operation canceled", "Operation canceled"));
                _logger.AddLoudReport(new LoudReport("Operation canceled"));
            }
            finally
            {
                ThreadSafeButtonToggle(true, false);
                _cts.Dispose();
            }

        }

        private void MagicContentBased()
        {
            this.RecommenderCombo.SelectedValue = RecommenderType.ContentBased;

            this.FeaturedDataUsersQuantity.Value = 10;
            this.MinimumItemsRated.Value = 10;
            this.Crossvalidation.Value = 80;

            this.ContentBased_MinFeatures.Value = 10;

            this.ContentBased_SigmoidAlpha.Value = 2;
            this.ContentBased_HiddenLayerNeurons.Value = 1;
            this.ContentBased_Iterations.Value = 1000;

            this.ContentBased_Momentum.Value = 0.9m;
            this.ContentBased_LearningRate.Value = 0.1m;
            this.ContentBased_PopulationSize.Value = 100;

            this.ContentBased_Teacher.SelectedValue = TeacherFunction.Genetic;


            string parametersString = string.Format(@"
Content-Based
Teacher: {0}
Users count: {1}
Minimum items rated: {2}
Minimum features: {3}
Sigmoid Alpha: {4}
Hidden Layer Neurons: {5}
Max Iterations: {6}
Momentum: {7}
Learning Rate: {8}
GA PopulationSize: {9}
", ContentBased_Teacher.SelectedValue.ToString(), FeaturedDataUsersQuantity.Value, MinimumItemsRated.Value, ContentBased_MinFeatures.Value, ContentBased_SigmoidAlpha.Value, ContentBased_HiddenLayerNeurons.Value, ContentBased_Iterations.Value, ContentBased_Momentum.Value, ContentBased_LearningRate.Value, ContentBased_PopulationSize.Value);



            _logger.SilentMode = true;
            _logger.AddLoudReport(new LoudReport(parametersString));

            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            SetupRecommenderEngine();

            ///test MinFeatures param
            Task.Run(() =>
            {

                ThreadSafeButtonToggle(false, true);

                int[] vals = new int[] { 100, 500, 1000, 3000, 10000};
                //int[] vals = new int[] { 50, 75, 100, 150 , 200, 300 };

                var changableName = "iterations";
                //var changableName = "GA Population";

                _logger.AddLoudReport(new LoudReport(string.Format("{0} RMSE MAE CBD time", changableName)));

                foreach (var i in vals)
                {
                    ((NeuroRecommender)_recommenderEngine.Recommender).IterationLimit = i;
                    //_recommenderEngine.MinimumItemsRated = i;

                    _recommenderEngine.LoadData(token);
                    DoMagicAction(i, changableName, token);
                }

                _logger.SilentMode = false;
            });
        }

        private void MagicCollaborative()
        {
            this.RecommenderCombo.SelectedValue = RecommenderType.Collaborative;
            this.CollaborativeAlgorithmCombo.SelectedValue = CollaborativeAlgorithm.SVDplusplus;

            this.BasicDataUsersQuantity.Value = 20;
            this.MinimumItemsRated.Value = 1;
            this.Crossvalidation.Value = 5;

            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            SetupRecommenderEngine();

            ///test MinFeatures param
            Task.Run(() => {

                ThreadSafeButtonToggle(false, true);
                _logger.SilentMode = true;

                int[] vals = new int[] { 1, 20, 100, 500, 1000, 2000, 5000, 10000, 50000 };

                var changableName = "Users";

                _logger.AddLoudReport(new LoudReport(string.Format("{0} RMSE MAE CBD", changableName)));

                foreach (int i in vals)
                {
                    _recommenderEngine.BasicDataUsersQuantity = i;

                    _recommenderEngine.LoadData(token);
                    DoMagicAction(i, changableName, token);
                }

                _logger.SilentMode = false;
            });
        }

        private void MagicHybrid()
        {
            this.FeaturedDataUsersQuantity.Value = 50;
            this.MinimumItemsRated.Value = 5;
            this.Crossvalidation.Value = 5;

            this.ContentBased_MinFeatures.Value = 1;

            this.ContentBased_SigmoidAlpha.Value = 2;
            this.ContentBased_HiddenLayerNeurons.Value = 1;
            this.ContentBased_Iterations.Value = 1000;

            this.ContentBased_Momentum.Value = 0.9m;
            this.ContentBased_LearningRate.Value = 0.1m;
            this.ContentBased_PopulationSize.Value = 50;

            this.ContentBased_Teacher.SelectedValue = TeacherFunction.Genetic;

            this.BasicDataUsersQuantity.Value = 20;

            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            this.RecommenderCombo.SelectedValue = RecommenderType.Hybrid;
            SetupRecommenderEngine();

            ///test MinFeatures param
            Task.Run(() => {

                ThreadSafeButtonToggle(false, true);
                _logger.SilentMode = true;

                //var datasets = new List<DataSetType>() { DataSetType.MovieLense, DataSetType.AmazonMeta, DataSetType.YahooMusic };
                var datasets = new List<DataSetType>() {  DataSetType.YahooMusic };

                foreach (var dataset in datasets)
                {
                    _recommenderEngine.SetDataSet(dataset);


                    _logger.AddLoudReport(new LoudReport("DATASET: "+dataset.ToString()));

                    //skip DataSetType.MovieLense
                    //if (dataset != DataSetType.MovieLense)
                    //{
                        //Backprop
                        ((NeuroRecommender)((HybridRecommender)_recommenderEngine.Recommender).ContentRecommender).TeacherFunction = TeacherFunction.BackProp;

                        //_logger.AddLoudReport(new LoudReport("------------BACKPROPAGATION------------"));
                        //_logger.AddLoudReport(new LoudReport("------------MatrixFactorization------------"));
                        //((HybridRecommender)_recommenderEngine.Recommender).CollaborativeRecommender = new MatrixFactorization();

                        //RunHybridMagic1(token);

                        //_logger.AddLoudReport(new LoudReport("------------BACKPROPAGATION------------"));
                        //_logger.AddLoudReport(new LoudReport("------------BiasedMatrixFactorization------------"));
                        //((HybridRecommender)_recommenderEngine.Recommender).CollaborativeRecommender = new BiasedMatrixFactorization();

                        //RunHybridMagic1(token);

                        //_logger.AddLoudReport(new LoudReport("------------BACKPROPAGATION------------"));
                        //_logger.AddLoudReport(new LoudReport("------------SVDplusplus------------"));
                        //((HybridRecommender)_recommenderEngine.Recommender).CollaborativeRecommender = new SVDPlusPlus();

                        //RunHybridMagic1(token);



                        //Resillient Backprop
                        ((NeuroRecommender)((HybridRecommender)_recommenderEngine.Recommender).ContentRecommender).TeacherFunction = TeacherFunction.ResilientBackProp;

                        _logger.AddLoudReport(new LoudReport("------------RESILLIENT BACKPROPAGATION------------"));
                        _logger.AddLoudReport(new LoudReport("------------MatrixFactorization------------"));
                        ((HybridRecommender)_recommenderEngine.Recommender).CollaborativeRecommender = new MatrixFactorization();

                        RunHybridMagic1(token);

                        _logger.AddLoudReport(new LoudReport("------------RESILLIENT BACKPROPAGATION------------"));
                        _logger.AddLoudReport(new LoudReport("------------BiasedMatrixFactorization------------"));
                        ((HybridRecommender)_recommenderEngine.Recommender).CollaborativeRecommender = new BiasedMatrixFactorization();

                        RunHybridMagic1(token);

                        _logger.AddLoudReport(new LoudReport("------------RESILLIENT BACKPROPAGATION------------"));
                        _logger.AddLoudReport(new LoudReport("------------SVDplusplus------------"));
                        ((HybridRecommender)_recommenderEngine.Recommender).CollaborativeRecommender = new SVDPlusPlus();

                        RunHybridMagic1(token);

                    //}

                    //Genetic Backprop
                    //((NeuroRecommender)((HybridRecommender)_recommenderEngine.Recommender).ContentRecommender).TeacherFunction = TeacherFunction.Genetic;

                    //_logger.AddLoudReport(new LoudReport("------------GENETIC------------"));
                    //_logger.AddLoudReport(new LoudReport("------------MatrixFactorization------------"));
                    //((HybridRecommender)_recommenderEngine.Recommender).CollaborativeRecommender = new MatrixFactorization();

                    //RunHybridMagic1(token);

                    //_logger.AddLoudReport(new LoudReport("------------GENETIC------------"));
                    //_logger.AddLoudReport(new LoudReport("------------BiasedMatrixFactorization------------"));
                    //((HybridRecommender)_recommenderEngine.Recommender).CollaborativeRecommender = new BiasedMatrixFactorization();

                    //RunHybridMagic1(token);

                    //_logger.AddLoudReport(new LoudReport("------------GENETIC------------"));
                    //_logger.AddLoudReport(new LoudReport("------------SVDplusplus------------"));
                    //((HybridRecommender)_recommenderEngine.Recommender).CollaborativeRecommender = new SVDPlusPlus();

                    //RunHybridMagic1(token);
                }


                _logger.SilentMode = false;
                ThreadSafeButtonToggle(false, true);
            });
        }

        public void RunHybridMagic1(CancellationToken token)
        {
            int[] usersVals = new int[] { 50, 100, 500, 1000 }; //, , 5000, 10000, 20000 
            int[] minItemsRatedVals = new int[] { 1, 2, 5, 10, 20 };

            var changableName = "Users";
            var changableName2 = "MinimumItemsRated";


            //_logger.AddLoudReport(new LoudReport(string.Format("{0} {1} RMSE MAE CBD", changableName, changableName2)));

            //foreach (int i in usersVals)
            //{
            //    _recommenderEngine.BasicDataUsersQuantity = i;

            //    foreach (int j in minItemsRatedVals)
            //    {
            //        //_recommenderEngine.MinimumItemsRated = j;

            //        //_recommenderEngine.LoadData(token);

            //        //DoMagicAction2(i, j);
            //    }
            //}

            var changableName3 = "MinimumRepeatingFeatures";
            int[] minRepeatedFeatures = new int[] { 1, 2, 5, 10 };

            _logger.AddLoudReport(new LoudReport(string.Format("{0} {1} RMSE MAE CBD", changableName, changableName3)));

            foreach (int i in usersVals)
            {
                _recommenderEngine.BasicDataUsersQuantity = i;

                if (((HybridRecommender)_recommenderEngine.Recommender).CollaborativeRecommender is SVDPlusPlus && i > 1000)
                    continue;

                foreach (int j in minRepeatedFeatures)
                {
                    ((NeuroRecommender)((HybridRecommender)_recommenderEngine.Recommender).ContentRecommender).MinimumRepeatingFeatures = j;

                    _recommenderEngine.LoadData(token);

                    DoMagicAction2(i, j, token);
                }
            }
        }
    }
}
