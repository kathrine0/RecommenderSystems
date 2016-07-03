using MyMediaLite.RatingPrediction;
using Recommender.Core;
using Recommender.Core.Engine;
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

        public MainForm()
        {
            InitializeComponent();

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


            //this.datasetCombo.DataSource = Datasets;
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            

            //recommender.Recommender = new MatrixFactorization();
            //TODO figure out here some nice pattern
            

            SetupRecommender();

            this.StatusLabel.Text = "Loading data...";

            _recommender.LoadData();

            this.StatusLabel.Text = "Fetching results...";

            var result = _recommender.GetResults();

            this.StatusLabel.Text = "Finished";

            AddResultBoxText(result.ToString());
        }

        private void AddResultBoxText(string text)
        {
            this.ResultBox.AppendText(text);
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

            switch ((ContentBasedAlgorithm)(this.ContentBasedAlgorithmCombo.SelectedValue))
            {
                case ContentBasedAlgorithm.NeuralNetwork:
                    _recommender.Recommender = new NeuroRecommender();
                    AddResultBoxText("Selected algorithm: Neural Network");
                    break;
                case ContentBasedAlgorithm.NeuralNetworkWithBias:
                    AddResultBoxText("Selected algorithm: Biased Neural Network");
                    throw new NotImplementedException();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Unknown recommender type");
            }
        }

        private void ChooseHybridAlgorithm()
        {
            throw new NotImplementedException();
        }


        private void recommenderCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((RecommenderType) ((ComboBox)sender).SelectedValue)
            {
                case RecommenderType.Collaborative:
                    this.CollaborativeAlgorithmChoice.Enabled = true;
                    this.ContentBasedAlghoritmChoice.Enabled  = false;
                    break;
                case RecommenderType.ContentBased:
                    this.CollaborativeAlgorithmChoice.Enabled = false;
                    this.ContentBasedAlghoritmChoice.Enabled = true;
                    break;
                case RecommenderType.Hybrid:
                    this.CollaborativeAlgorithmChoice.Enabled = true;
                    this.ContentBasedAlghoritmChoice.Enabled = true;
                    break;
            }
        }
    }
}
