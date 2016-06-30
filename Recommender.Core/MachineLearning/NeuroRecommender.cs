using MyMediaLite.RatingPrediction;
using System;
using Accord;
using MyMediaLite.Data;
using Recommender.Service.Data;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using Accord.Math;
using Accord.Statistics.Filters;
using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;

namespace Recommender.Core.MachineLearning
{
    public class NeuroRecommender : RatingPredictor
    {

        public NeuroRecommender()
        {
        }


        public virtual IFeaturedRatings FeaturedRatings
        {
            get { return featured_ratings; }
            set
            {
                featured_ratings = value;
                Ratings = value;
            }
        }
        /// <summary>rating data, including item features</summary>
        protected IFeaturedRatings featured_ratings;

        ///
        public override IRatings Ratings
        {
            get { return ratings; }
            set
            {
                if (!(value is IFeaturedRatings))
                    throw new ArgumentException("Ratings must be of type IFeaturedRatings.");

                base.Ratings = value;
                featured_ratings = (IFeaturedRatings)value;
            }
        }

        protected double weight;


        public override float Predict(int user_id, int item_id)
        {
            throw new NotImplementedException();
        }

        public override void Train()
        {
            InitModel();//ratings.RandomIndex

            throw new NotImplementedException();
        }


        protected internal virtual void InitModel()//IList<int> rating_indices
        {

            var tablePerUser = new Dictionary<int, DataTable>();

            var tmpinputColumns = new List<string>(featured_ratings.Features[0].Select(x => x.Key));
            string[] inputColumns = tmpinputColumns.ToArray();
            string outputColumn = "rating";

            //build separate table for every user  //move it to featuredRatings class
            foreach (var userRatings in featured_ratings.ByUser)
            {
                if (userRatings.Count == 0)
                    continue;

                int userId = featured_ratings.Users[userRatings.First()];

                //prepare datatable structure
                DataTable table = new DataTable();


                table.Columns.Add(inputColumns);
                table.Columns.Add(outputColumn);

                //and items to datatable
                foreach (var index in userRatings)
                {
                    //if (Single.IsNaN(featured_ratings[index]))
                    //    continue;

                    //if (featured_ratings.Features[index].Any(x => x.Value)))
                    //    continue;


                    int i = featured_ratings.Items[index];
                    var features = featured_ratings.Features[index]
                        .Where(x => x.Value != null)
                        .Select(x => x.Value.ToString()); //delete to string

                    var data = new List<string>(features);
                    data.Add(featured_ratings[index].ToString());

                    table.Rows.Add(data.ToArray());

                }


                tablePerUser.Add(userId, table);
            }




            foreach (var table in tablePerUser)
            {
                //// Now, we have to convert the textual, categorical data found
                //// in the table to a more manageable discrete representation.
                //// 
                //// For this, we will create a codebook to translate text to
                //// discrete integer symbols:
                //// 
                Codification codebook = new Codification(table.Value);

                //// And then convert all data into symbols
                //// 
                DataTable symbols = codebook.Apply(table.Value);
                double[][] inputs = symbols.ToArray(inputColumns);
                int[] outputs = symbols.ToArray<int>(outputColumn);


                var attributes = DecisionVariable.FromCodebook(codebook, inputColumns);
                DecisionTree tree = new DecisionTree(attributes, classes: 10); //todo calculate classes //todo switch from discrete values to constant


                //// Now, let's create the C4.5 algorithm
                C45Learning c45 = new C45Learning(tree);


                double error = c45.Run(inputs, outputs);
                int y = tree.Compute(inputs[25]);
                Func<double[], int> func = tree.ToExpression().Compile();
                int z = func(inputs[25]);
            }

            //string nurseryData = Resources.nursery;

            if (FeaturedRatings.Features.Count < 1)
                throw new ArgumentOutOfRangeException("Not enough features");

            
        }
    }
}
