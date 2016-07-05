using MyMediaLite.RatingPrediction;
using Recommender.Core.Engine;
using Recommender.Core.MachineLearning;
using Recommender.GUI.Enums;
using Recommender.GUI.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Recommender.GUI
{
    public partial class MainForm : Form    
    {

        RecommenderEngine _recommenderEngine;

        public MainForm()
        {
            InitializeComponent();
            InitializeCombos();
        }

        private void InitializeCombos()
        {
            this.datasetCombo.DataSource = DataSetOption.OptionBuilder();
            this.datasetCombo.DisplayMember = "Name";
            this.datasetCombo.ValueMember = "Value";

            this.recommenderCombo.DataSource = RecommenderTypeOption.OptionBuilder();
            this.recommenderCombo.DisplayMember = "Name";
            this.recommenderCombo.ValueMember = "Value";

            this.CollaborativeAlgorithmCombo.DataSource = CollaborativeAlgorithmOption.OptionBuilder();
            this.CollaborativeAlgorithmCombo.DisplayMember = "Name";
            this.CollaborativeAlgorithmCombo.ValueMember = "Value";

            this.ContentBasedAlgorithmCombo.DataSource = ContentBasedAlgorithmOption.OptionBuilder();
            this.ContentBasedAlgorithmCombo.DisplayMember = "Name";
            this.ContentBasedAlgorithmCombo.ValueMember = "Value";
        }

        private void Progress_ProgressChanged(ProgressState progressState)
        {
            ProgressBar.Value = progressState.Percentage;

            var statusText = StatusLabel.Text;

            if (!string.IsNullOrEmpty(progressState.StatusText))
                statusText = progressState.StatusText;

            StatusLabel.Text = progressState.StatusText + " " + ProgressBar.Value.ToString() + "%";

            if (!string.IsNullOrEmpty(progressState.ResultBoxText))
                AddResultBoxText(progressState.ResultBoxText);

            //When finished
            if (progressState.Percentage == 100)
            {
                RunButton.Enabled = true;
                CancelButton.Enabled = false;
            }
        }

        private void AddResultBoxText(string text)
        {
            this.ResultBox.AppendText(text + "\n");
        }

        private void SetupRecommender()
        {
            switch ((RecommenderType)(this.recommenderCombo.SelectedValue))
            {
                case RecommenderType.Collaborative:
                    AddResultBoxText("Selected recommender: Collaborative Filtering");
                    ChooseCollaborativeAlgorithm();
                    break;
                case RecommenderType.ContentBased:
                    AddResultBoxText("Selected recommender: Content-Based Filtering");
                    ChooseContentBasedAlgorithm();
                    break;
                case RecommenderType.Hybrid:
                    AddResultBoxText("Selected recommender: Hybrid recommender");
                    ChooseHybridAlgorithm();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Unknown recommender type");
            }

            _recommenderEngine.SetTrainingSetRatio(this.TrainingSetSize.Value);
        }

        private void ChooseCollaborativeAlgorithm()
        {
            _recommenderEngine = new CollaborativeRecommenderEngine();

            switch ((CollaborativeAlgorithm)(this.CollaborativeAlgorithmCombo.SelectedValue))
            {
                case CollaborativeAlgorithm.MatrixFactorization:
                    _recommenderEngine.Recommender = new MatrixFactorization();
                    AddResultBoxText("Selected algorithm: Matrix Factorization");
                    break;
                case CollaborativeAlgorithm.BiasedMatrixFactorization:
                    _recommenderEngine.Recommender = new BiasedMatrixFactorization();
                    AddResultBoxText("Selected algorithm: Biased Matrix Factorization");
                    break;
                case CollaborativeAlgorithm.SVDplusplus:
                    _recommenderEngine.Recommender = new SVDPlusPlus();
                    AddResultBoxText("Selected algorithm: SVD++");
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Unknown recommender type");
            }
        }

        private void ChooseContentBasedAlgorithm()
        {
            _recommenderEngine = new ContentRecommenderEngine();
            _recommenderEngine.Recommender = new NeuroRecommender();

            switch ((ContentBasedAlgorithm)(this.ContentBasedAlgorithmCombo.SelectedValue))
            {
                case ContentBasedAlgorithm.NeuralNetwork:
                    AddResultBoxText("Selected algorithm: Neural Network");
                    break;
                case ContentBasedAlgorithm.NeuralNetworkWithBias:
                    AddResultBoxText("Selected algorithm: Biased Neural Network");
                    throw new NotImplementedException();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Unknown recommender type");
            }

            //data settings
            ((ContentRecommenderEngine)_recommenderEngine).NumberOfUsers = decimal.ToInt32(this.ContentBased_AmountOfUsers.Value);
            ((ContentRecommenderEngine)_recommenderEngine).MinimumItemsRated = decimal.ToInt32(this.ContentBased_MinimumItemsRated.Value);
            ((NeuroRecommender)_recommenderEngine.Recommender).MinimumRepeatingFeatures = decimal.ToInt32(this.ContentBased_MinFeatures.Value);

            //neuro settings
            ((NeuroRecommender)_recommenderEngine.Recommender).Momentum = decimal.ToDouble(this.ContentBased_Momentum.Value);
            ((NeuroRecommender)_recommenderEngine.Recommender).SigmoidAlphaValue = decimal.ToDouble(this.ContentBased_SigmoidAlpha.Value);
            ((NeuroRecommender)_recommenderEngine.Recommender).LearningErrorLimit = decimal.ToDouble(this.ContentBased_LearningErrorLimit.Value);
            ((NeuroRecommender)_recommenderEngine.Recommender).HiddenLayerNeurons = decimal.ToInt32(this.ContentBased_HiddenLayerNeurons.Value);
            ((NeuroRecommender)_recommenderEngine.Recommender).IterationLimit = decimal.ToInt32(this.ContentBased_Iterations.Value);

        }

        private void ChooseHybridAlgorithm()
        {
            throw new NotImplementedException();
        }

        private void DoWork(IProgress<ProgressState> progress, CancellationToken cts)
        {
            progress.Report(new ProgressState(10, null, "Loading data..."));

            _recommenderEngine.LoadData();

            progress.Report(new ProgressState(20, null, "Training..."));

            _recommenderEngine.TeachRecommender();

            progress.Report(new ProgressState(80, null, "Evaluating..."));

            var result = _recommenderEngine.GetResults();

            var resultString = result.ToString() + "\n =========================================";

            progress.Report(new ProgressState(100, resultString, "Idle"));
        }


        #region GUI Interactions

        private void RunButton_Click(object sender, EventArgs e)
        {
            var synchronizationContext = TaskScheduler.FromCurrentSynchronizationContext();

            SetupRecommender();

            IProgress<ProgressState> progress = new Progress<ProgressState>(x => Progress_ProgressChanged(x));
            
            var cts = new CancellationToken();

            var t = Task.Run(() => DoWork(progress, cts));
  
            RunButton.Enabled = false;
            CancelButton.Enabled = true;
        }

        private void recommenderCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = (RecommenderTypeOption)recommenderCombo.SelectedItem;

            switch (selected.Value)
            {
                case RecommenderType.Collaborative:
                    this.CollaborativeAlgorithmChoice.Enabled = true;
                    this.ContentBasedAlghoritmChoice.Enabled  = false;

                    this.CollaborativeOptions.Visible = true;
                    this.ContentBasedOptions.Visible = false;
                    break;
                case RecommenderType.ContentBased:
                    this.CollaborativeAlgorithmChoice.Enabled = false;
                    this.ContentBasedAlghoritmChoice.Enabled = true;

                    this.CollaborativeOptions.Visible = false;
                    this.ContentBasedOptions.Visible = true;
                    break;
                case RecommenderType.Hybrid:
                    this.CollaborativeAlgorithmChoice.Enabled = true;
                    this.ContentBasedAlghoritmChoice.Enabled = true;

                    this.CollaborativeOptions.Visible = true;
                    this.ContentBasedOptions.Visible = true;
                    break;
            }
        }

        private void TrainingSetSize_ValueChanged(object sender, EventArgs e)
        {
            var value = ((NumericUpDown)sender).Value;
            this.TestingSetSize.Value = 100 - value;
        }

        private void TestingSetSize_ValueChanged(object sender, EventArgs e)
        {
            var value = ((NumericUpDown)sender).Value;
            this.TrainingSetSize.Value = 100 - value;
        }
        private void CancelButton_Click(object sender, EventArgs e)
        {
            //if (backgroundWorker.IsBusy)
            //{
            //    backgroundWorker.CancelAsync();
            //}
        }

        #endregion
        
    }
}
