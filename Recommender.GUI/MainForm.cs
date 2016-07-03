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
            RecommenderEngine recommender = new ContentRecommenderEngine();

            //recommender.Recommender = new MatrixFactorization();
            //TODO figure out here some nice pattern
            recommender.Recommender = new NeuroRecommender();
            this.StatusLabel.Text = "Loading data...";

            recommender.LoadData();

            this.StatusLabel.Text = "Fetching results...";

            var result = recommender.GetResults();

            this.StatusLabel.Text = "Finished";
            this.ResultBox.AppendText(result.ToString());
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
