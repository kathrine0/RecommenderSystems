using MyMediaLite.RatingPrediction;
using Recommender.Core;
using Recommender.Core.Engine;
using Recommender.Core.Enums;
using Recommender.Core.MachineLearning;
using Recommender.GUI.Enums;
using Recommender.GUI.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Recommender.GUI
{
    public partial class MainForm : Form    
    {

        RecommenderEngine _recommender;

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

            _recommender.SetTrainingSetRatio(this.TrainingSetSize.Value);
        }

        private void ChooseCollaborativeAlgorithm()
        {
            _recommender = new CollaborativeRecommenderEngine();

            switch ((CollaborativeAlgorithm)(this.CollaborativeAlgorithmCombo.SelectedValue))
            {
                case CollaborativeAlgorithm.MatrixFactorization:
                    _recommender.Recommender = new MatrixFactorization();
                    AddResultBoxText("Selected algorithm: Matrix Factorization");
                    break;
                case CollaborativeAlgorithm.BiasedMatrixFactorization:
                    _recommender.Recommender = new BiasedMatrixFactorization();
                    AddResultBoxText("Selected algorithm: Biased Matrix Factorization");
                    break;
                case CollaborativeAlgorithm.SVDplusplus:
                    _recommender.Recommender = new SVDPlusPlus();
                    AddResultBoxText("Selected algorithm: SVD++");
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Unknown recommender type");
            }
        }

        private void ChooseContentBasedAlgorithm()
        {
            _recommender = new ContentRecommenderEngine();
            _recommender.Recommender = new NeuroRecommender();

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

            ((ContentRecommenderEngine)_recommender).NumberOfUsers = decimal.ToInt32(this.ContentBased_AmountOfUsers.Value);
            ((ContentRecommenderEngine)_recommender).MinimumItemsRated = decimal.ToInt32(this.ContentBased_MinimumItemsRated.Value);

            ((NeuroRecommender)_recommender.Recommender).Momentum = decimal.ToDouble(this.ContentBased_Momentum.Value);
            ((NeuroRecommender)_recommender.Recommender).SigmoidAlphaValue = decimal.ToDouble(this.ContentBased_SigmoidAlpha.Value);
            ((NeuroRecommender)_recommender.Recommender).LearningErrorLimit = decimal.ToDouble(this.ContentBased_LearningErrorLimit.Value);
            ((NeuroRecommender)_recommender.Recommender).HiddenLayerNeurons = decimal.ToInt32(this.ContentBased_HiddenLayerNeurons.Value);
            ((NeuroRecommender)_recommender.Recommender).MinimumRepeatingFeatures = decimal.ToInt32(this.ContentBased_MinFeatures.Value);

        }

        private void ChooseHybridAlgorithm()
        {
            throw new NotImplementedException();
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

        private void DoWork(IProgress<ProgressState> progress, CancellationToken cts)
        {
            progress.Report(new ProgressState(10, null, "Loading data..."));

            _recommender.LoadData();

            progress.Report(new ProgressState(20, null, "Training and evaluating..."));

            var result = _recommender.GetResults();

            var resultString = result.ToString() + "\n =========================================";

            progress.Report(new ProgressState(100, resultString, "Idle"));
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
