﻿using Recommender.Common.Logger;
using Recommender.Core.Engine;
using Recommender.Core.Enums;
using Recommender.Core.RatingPrediction.ContentBased;
using Recommender.Core.RatingPrediction.Collaborative;
using Recommender.GUI.Enums;
using Recommender.GUI.Extensions;
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
            CreateCombo(this.recommenderCombo, RecommenderTypeOption.OptionBuilder());
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
            if (!string.IsNullOrEmpty(progressState.ResultBoxText))
                this.ResultBox.PerformSafely(() => AddResultBoxText(progressState.ResultBoxText));

            if (_progressStore >= 100)
                _progressStore = 0;
        }

        private void ThreadSafeIncrementProgress(double progressStep)
        {
            this.StatusStrip.PerformSafely(() =>
            {
                _progressStore += progressStep;
                this.ProgressBar.Value = (int)Math.Round(_progressStore);

                this.ProcentLabel.Text = ProgressBar.Value.ToString() + "%";
            });


            if (_progressStore >= 100)
                _progressStore = 0;
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

        #endregion logger

        private void AddResultBoxText(string text)
        {
            this.ResultBox.AppendText(text + "\n");
        }


        private void SetupRecommenderEngine()
        {
            switch ((RecommenderType)(this.recommenderCombo.SelectedValue))
            {
                case RecommenderType.Collaborative:
                    ChooseCollaborativeAlgorithm();
                    break;
                case RecommenderType.ContentBased:
                    ChooseContentBasedAlgorithm();
                    SetupNeuroSettings();
                    break;
                case RecommenderType.Hybrid:
                    ChooseHybridAlgorithm();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Unknown recommender type");
            }

            //data settings
            _recommenderEngine.NumberOfUsers = decimal.ToInt32(this.ContentBased_AmountOfUsers.Value);
            _recommenderEngine.MinimumItemsRated = decimal.ToInt32(this.ContentBased_MinimumItemsRated.Value);
            _recommenderEngine.SetTrainingSetRatio(this.TrainingSetSize.Value);
        }

        private void ChooseCollaborativeAlgorithm()
        {
            _recommenderEngine = new CollaborativeRecommenderEngine(_logger);

            switch ((CollaborativeAlgorithm)(this.CollaborativeAlgorithmCombo.SelectedValue))
            {
                case CollaborativeAlgorithm.MatrixFactorization:
                    _recommenderEngine.Recommender = new MatrixFactorization();
                    break;
                case CollaborativeAlgorithm.BiasedMatrixFactorization:
                    _recommenderEngine.Recommender = new BiasedMatrixFactorization();
                    break;
                case CollaborativeAlgorithm.SVDplusplus:
                    _recommenderEngine.Recommender = new SVDPlusPlus();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Unknown recommender type");
            }
        }

        private void ChooseContentBasedAlgorithm()
        {
            _recommenderEngine = new ContentRecommenderEngine(_logger);
            _recommenderEngine.Recommender = new NeuroRecommender();
        }

        private void SetupNeuroSettings()
        {
            ((NeuroRecommender)_recommenderEngine.Recommender).MinimumRepeatingFeatures = decimal.ToInt32(this.ContentBased_MinFeatures.Value);

            //neuro settings
            ((NeuroRecommender)_recommenderEngine.Recommender).SigmoidAlphaValue = decimal.ToDouble(this.ContentBased_SigmoidAlpha.Value);
            ((NeuroRecommender)_recommenderEngine.Recommender).HiddenLayerNeurons = decimal.ToInt32(this.ContentBased_HiddenLayerNeurons.Value);
            ((NeuroRecommender)_recommenderEngine.Recommender).IterationLimit = decimal.ToInt32(this.ContentBased_Iterations.Value);

            //teacher settings
            ((NeuroRecommender)_recommenderEngine.Recommender).Momentum = decimal.ToDouble(this.ContentBased_Momentum.Value);
            ((NeuroRecommender)_recommenderEngine.Recommender).LearningRate = decimal.ToDouble(this.ContentBased_LearningRate.Value);
            ((NeuroRecommender)_recommenderEngine.Recommender).PopulationSize = decimal.ToInt32(this.ContentBased_PopulationSize.Value);
            ((NeuroRecommender)_recommenderEngine.Recommender).TeacherFunction = ((TeacherFunctionOption)ContentBased_Teacher.SelectedItem).Value;
        }

        private void ChooseHybridAlgorithm()
        {
            throw new NotImplementedException();
        }

        #region Long actions

        private void DoWork(CancellationToken token)
        {
            _recommenderEngine.LoadData(token);

            var teachingResult = _recommenderEngine.TeachRecommender();

            if (teachingResult)
                _recommenderEngine.GetResults();
        }

        #endregion Long actions

        #region GUI Interactions

        private void RunButton_Click(object sender, EventArgs e)
        {
            SetupRecommenderEngine();

            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            Task.Run(() =>
            {
                ThreadSafeButtonToggle(false, true);

                try
                {
                    DoWork(token);
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
            });
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
            _cts.Cancel();
            ThreadSafeButtonToggle(false, false);
        }

        private void ThreadSafeButtonToggle(bool runbtn, bool cancelbtn)
        {
            //this.LoadDataButton.PerformSafely(() => this.LoadDataButton.Enabled = loadbtn);
            this.RunButton.PerformSafely(() => this.RunButton.Enabled = runbtn);
            this.CancelButton.PerformSafely(() => this.CancelButton.Enabled = cancelbtn);
        }

        #endregion
    }
}
