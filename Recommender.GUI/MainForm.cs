using MyMediaLite.RatingPrediction;
using Recommender.Core;
using Recommender.Core.MachineLearning;
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
        public IList<string> Alghoritms
        {
            get
            {
                return new List<string>()
                {
                    "Matrix Factorization",
                    "Biased Matrix Factorization",
                    "SVD++",
                    "Decision Tree",
                    "Hybrid"
                };
            }
        }
        public IList<string> Datasets
        {
            get
            {
                return new List<string>()
                {
                    "MovieLense",
                    "AmazonMeta",
                    "LastFm"
                };
            }
        }

        public MainForm()
        {
            InitializeComponent();

            this.alghoritmCombo.DataSource = Alghoritms;
            this.datasetCombo.DataSource = Datasets;


        }

        private void LearnButton_Click(object sender, EventArgs e)
        {
            var recommender = new RecommenderEngine();

            //recommender.Recommender = new MatrixFactorization();
            recommender.Recommender = new C45Recommender();
            this.LogBox.AppendText("Loading data...");
            recommender.LoadData();
            var result = recommender.GetResults();
            this.ResultBox.AppendText(result.ToString());
        }
    }
}
