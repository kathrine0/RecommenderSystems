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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Recommender.GUI
{
    public partial class MainForm : Form    
    {

        RecommenderEngine _recommender;

        BackgroundWorker backgroundWorker;

        public MainForm()
        {
            InitializeComponent();
            InitializeCombos();
            InitializeBackgroundWorker();
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

            this.ContentBased_ActivationFunction.DataSource = ActivationFunctionOption.OptionBuilder();
            this.ContentBased_ActivationFunction.DisplayMember = "Name";
            this.ContentBased_ActivationFunction.ValueMember = "Value";
        }

        private void InitializeBackgroundWorker()
        {
            backgroundWorker = new BackgroundWorker();

            backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker_ProgressChanged);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
        }

        #region backgroundWorker
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                AddResultBoxText("Task Cancelled");
            }
            else if (e.Error != null)
            {
                AddResultBoxText("Error while performing background operation.");
            }

            this.RunButton.Enabled = true;
            this.CancelButton.Enabled = false;
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressBar.Value = e.ProgressPercentage;

            var progressState = e.UserState as ProgressState;

            StatusLabel.Text = progressState.StatusText + " " + ProgressBar.Value.ToString() + "%";

            if (!string.IsNullOrEmpty(progressState.ResultBoxText))
                AddResultBoxText(progressState.ResultBoxText);
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            backgroundWorker.ReportProgress(10, new ProgressState(null, "Loading data..."));
            
            _recommender.LoadData();

            backgroundWorker.ReportProgress(20, new ProgressState(null, "Training and evaluating..."));

            var result = _recommender.GetResults();

            var resultString = result.ToString() + "\n =========================================";

            backgroundWorker.ReportProgress(100, new ProgressState(resultString, "Idle"));

            //if (backgroundWorker.CancellationPending)
            //{
            //    e.Cancel = true;
            //    backgroundWorker.ReportProgress(0);
            //    return;
            //}

            //backgroundWorker.ReportProgress(100);
        }

        #endregion


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

            ((ContentRecommenderEngine)_recommender).NumberOfUsers = Decimal.ToInt32(this.ContentBased_AmountOfUsers.Value);
            ((ContentRecommenderEngine)_recommender).MinimumItemsRated = Decimal.ToInt32(this.ContentBased_MinimumItemsRated.Value);
            ((NeuroRecommender) _recommender.Recommender).ActivationFunctionType = (ActivationFunction)this.ContentBased_ActivationFunction.SelectedValue;
        }

        private void ChooseHybridAlgorithm()
        {
            throw new NotImplementedException();
        }


        #region GUI Interactions

        private void RunButton_Click(object sender, EventArgs e)
        {
            RunButton.Enabled = false;
            CancelButton.Enabled = true;

            SetupRecommender();

            backgroundWorker.RunWorkerAsync();
        }

        private void recommenderCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((RecommenderType) ((ComboBox)sender).SelectedValue)
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
            if (backgroundWorker.IsBusy)
            {
                backgroundWorker.CancelAsync();
            }
        }

        #endregion

    }
}
